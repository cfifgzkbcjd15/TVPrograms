using Microsoft.EntityFrameworkCore;
using TVPrograms.Models.Channels;
using TVPrograms.Models.Chats;
using TVPrograms.Models.Events;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public async Task<List<ResponseChannel>> GetChannels(FilterChannels filters)
        {
            int size = 20;
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                IQueryable<Channel> response = db.Channels.OrderBy(x => x.Sort);
                if (!string.IsNullOrEmpty(filters.SearchText))
                {
                    response = response.Where(x => x.Name.ToLower().Contains(filters.SearchText.ToLower()));
                }
                if (filters.Page > 0)
                {
                    response = response.Skip(filters.Page * size).Take(size);
                }
                else
                {
                    response = response.Take(size);
                }
                var currentDate = DateTime.Now;
                return await response.AsNoTracking().Include(x => x.Events).Select(x =>
                    new ResponseChannel
                    {
                        Name = x.Name,
                        SmallImg = x.SmallImg,
                        Img = x.Img,
                        LargeImg = x.LargeImg,
                        Id = x.ChannelId,
                        CurrentEvent = x.Events.Where(f => currentDate < f.EndDate).OrderBy(x => x.StartDate).Select(s => new ResponseEvent
                        {
                            Id = s.Id,
                            Description = s.Title,
                            Name = s.Name,
                            ChannelId = s.ChannelId,
                            StartDateMs= ((DateTimeOffset)s.StartDate).ToUnixTimeMilliseconds(),
                            StartDate = s.StartDate.ToString("HH:mm"),
                            EndDate = s.EndDate.ToString("HH:mm"),
                            Procent = ((s.EndDate - currentDate).TotalMinutes / (s.EndDate - s.StartDate).TotalMinutes) * 100
                        }).Take(2).ToList(),
                    }
                ).ToListAsync();
            }
        }
        //public async Task<List<Channel>> GetChannels(int id)
        //{
        //    using (LimeHdTvContext db = new LimeHdTvContext(options))
        //    {
        //        return await db.Channels.FirstOrDefaultAsync(x=>x.ChannelId=id);
        //    }
        //}

        public async Task<List<int>> GetChannelsId()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Channels.AsNoTracking().Select(x => x.ChannelId).ToListAsync();
            }
        }

        public async Task AddChannelsToMailApi(List<ChannelJson> channel)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                var sort = 1;
                await db.Channels.AddRangeAsync(channel.Select(x => new Channel { ChannelId = x.ChannelId, Img = x.Img, LargeImg = x.LargeImg, Name = x.Name, SmallImg = x.SmallImg, Sort = sort++ }));
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteChannels()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                db.RemoveRange(await db.Channels.AsNoTracking().ToListAsync());
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Event>> GetAllChannels()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Events.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<ResponseAiEvent>> GetAllEvents()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                var events = await db.Events.Select(x => new ResponseAiEvent { Id = x.Id, Description = x.Title }).Take(2000).AsNoTracking().ToListAsync();
                var result = new List<ResponseAiEvent>();
                foreach (var item in events)
                {
                    if (result.Count > 100)
                    {
                        return result;
                    }
                    if (result.Where(x => x.Description == item.Description).Count() == 0)
                    {
                        result.Add(item);
                    }

                }
                return null;
            }
        }
    }
}

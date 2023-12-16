using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using TVPrograms.Data;
using TVPrograms.Models.Chats;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public async Task<List<Channel>> GetChannels(FilterChannels filters)
        {
            int size = 20;
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                IQueryable<Channel> response = db.Channels;
                if (filters.Page > 0)
                {
                    response = response.Skip(filters.Page * size).Take(size);
                }
                else
                {
                    response = response.Take(size);
                }

                return await response.AsNoTracking().ToListAsync();
            }
        }
        public async Task<List<Channel>> GetAllChannels()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Channels.AsNoTracking().ToListAsync();
            }
        }

        public async Task AddChannelsToMailApi(List<ChannelJson> channel)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                var sort = 1;
                await db.Channels.AddRangeAsync(channel.Select(x=>new Channel { ChannelId=x.ChannelId, Img=x.Img, LargeImg=x.LargeImg, Name=x.Name, SmallImg=x.SmallImg, Sort= sort++}));
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
    }
}

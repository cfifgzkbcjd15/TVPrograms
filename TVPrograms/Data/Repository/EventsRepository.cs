using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Channels;
using TVPrograms.Models.Category;
using TVPrograms.Models.Events;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public async Task AddEvents(List<Event> events)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                await db.Events.AddRangeAsync(events);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<ResponseEvent>> ByChannelId(int channelId, int days = 1)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                var currentDate = DateTime.Now;
                return await db.Events
                    .Where(x => x.ChannelId == channelId && (x.StartDate.Day - DateTime.Now.Day) <= (days-1) && currentDate < x.EndDate)
                    .Include(x => x.Category)
                .OrderBy(x => x.StartDate)
                .Select(x => new ResponseEvent
                {
                    Id = x.Id,
                    Description = x.Title,
                    Name = x.Name,
                    ChannelId = channelId,
                    StartDateMs= ((DateTimeOffset)x.StartDate.AddHours(3)).ToUnixTimeMilliseconds(),
                    StartDate = x.StartDate.ToString("HH:mm"),
                    EndDate = x.EndDate.ToString("HH:mm"),
                    Category = new ResponseCategory { Name = x.Category.Name, Id = x.Category.Id }
                })
                .AsNoTracking()
                .ToListAsync();
            }
        }

        public async Task<ResponseEvent> ByEventId(Guid eventId)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Events
                    .Where(x => x.Id == eventId)
                    .Include(x => x.Category)
                    .OrderBy(x => x.StartDate)
                    .Select(x => new ResponseEvent
                    {
                        Id = x.Id,
                        Description = x.Title,
                        Name = x.Name,
                        ChannelId = x.ChannelId,
                        StartDateMs= ((DateTimeOffset)x.StartDate.AddHours(3)).ToUnixTimeMilliseconds(),
                        StartDate = x.StartDate.ToString("HH:mm"),
                        EndDate = x.EndDate.ToString("HH:mm"),
                        Category = new ResponseCategory { Name = x.Category.Name, Id = x.Category.Id }
                    }).AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<int> CheckCountCategoryes()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Categories.CountAsync();
            }
        }
    }
}

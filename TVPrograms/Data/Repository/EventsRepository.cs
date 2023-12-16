using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Channels;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public async Task<Event> AddEvents()
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                var channels = await db.Channels.AsNoTracking().ToListAsync();
                var currentDate= DateTime.Now;
                var uri = "https://tv.mail.ru/ajax/channel/?region_id=140&channel_type";
                channels.ForEach(channel =>
                {
                    for (var i = 0; i < 7; i++) {
                        currentDate.AddDays(i);
                    }
                });
            }
        }
    }
}

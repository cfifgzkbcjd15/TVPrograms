using Newtonsoft.Json;
using TVPrograms.Data;
using TVPrograms.Data.Repository;
using TVPrograms.Models.Chats;
using TVPrograms.Models.Events;

namespace TVPrograms.Services
{
    public class RandomDataProgramms
    {
        public async Task AddData()
        {

        }
        public async Task<List<Event>> GetRandomEvents(List<int> channels,int categoriesCount)
        {
            var events = new List<Event>();
            var file = File.ReadAllText("D:\\Desktop\\Proekts\\LimeHd\\TVPrograms\\wwwroot\\epg.json");
            var data = new List<EventJsonEpg>();

            JsonConvert.DeserializeObject<EventsJsonEpg>(file).Epg.ForEach(x =>
                data.AddRange(x.data.Select(s => new EventJsonEpg { Name = s.Title, Description = s.Desc, Date = s.Date }).ToList()));

            var countEvents = data.Count;

            foreach (var chanel in channels)
            {
                var random = new Random();
                var dates = GetRandomDate();
                foreach (var date in dates)
                {
                    var eve = data[random.Next(0, countEvents)];
                    events.Add(new Event
                    {
                        Id = Guid.NewGuid(),
                        ChannelId = chanel,
                        StartDate = date.Key,
                        EndDate = date.Value,
                        Name = eve.Name,
                        Title = eve.Description,
                        CategoryId =(short)random.Next(1, categoriesCount)
                    });
                }
            }
            return events;
        }

        public Dictionary<DateTime,DateTime> GetRandomDate()
        {
            var currentDate = DateTime.Now;
            var date = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0);
            var nextDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0);
            var dates = new Dictionary<DateTime, DateTime>();
            var random = new Random();
            var max = 120;
            var min = 20;
            nextDate = nextDate.AddMinutes(random.Next(min, max));
            while (true)
            {
                if (date.Day + 6 < nextDate.Day)
                    break;
                dates.Add(nextDate,nextDate = nextDate.AddMinutes(random.Next(min, max)));
            }
            return dates;
        }
    }
}

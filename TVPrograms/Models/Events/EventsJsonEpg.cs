using Newtonsoft.Json;
using TVPrograms.Models.Chats;

namespace TVPrograms.Models.Events
{
    public class EventJsonEpg
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }

    public class Datum
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("mskdatetimestart")]
        public string Date { get; set; }
    }

    public class Epg
    {
        [JsonProperty("data")]
        public List<Datum> data { get; set; }
    }

    public class EventsJsonEpg
    {
        [JsonProperty("epg")]
        public List<Epg> Epg { get; set; }
    }
}

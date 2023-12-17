using TVPrograms.Models.Events;

namespace TVPrograms.Models.Channels
{
    public class ResponseChannel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string SmallImg { get; set; }
        public string Img { get; set; }
        public string LargeImg { get; set; }
        public List<ResponseEvent> CurrentEvent { get; set; }
    }
}

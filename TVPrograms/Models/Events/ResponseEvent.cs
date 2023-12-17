using TVPrograms.Models.Category;

namespace TVPrograms.Models.Events
{
    public class ResponseEvent
    {
        public Guid Id { get; set; }
        public long EventId { get; set; }
        public int ChannelId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ResponseCategory Category { get; set; }
        public double Procent { get; set; }
    }
}

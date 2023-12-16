using System;
using System.Collections.Generic;

namespace TVPrograms.Data
{
    public partial class Event
    {
        public Guid Id { get; set; }
        public long EventId { get; set; }
        public int ChannelId { get; set; }
        public DateTime? Date { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public int? CategoryId { get; set; }

        public virtual Channel Channel { get; set; } = null!;
    }
}

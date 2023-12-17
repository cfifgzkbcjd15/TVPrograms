using System;
using System.Collections.Generic;

namespace TVPrograms.Data
{
    public partial class Event
    {
        public Guid Id { get; set; }
        public int ChannelId { get; set; }
        public DateTime StartDate { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public short? CategoryId { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Channel Channel { get; set; } = null!;
    }
}

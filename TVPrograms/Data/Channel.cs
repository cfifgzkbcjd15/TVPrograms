using System;
using System.Collections.Generic;

namespace TVPrograms.Data
{
    public partial class Channel
    {
        public Channel()
        {
            Events = new HashSet<Event>();
        }

        public string? Name { get; set; }
        public int ChannelId { get; set; }
        public string? SmallImg { get; set; }
        public string? Img { get; set; }
        public string? LargeImg { get; set; }
        public int? Sort { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TVPrograms.Data
{
    public partial class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        public short Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

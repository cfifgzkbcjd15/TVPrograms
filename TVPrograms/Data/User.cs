using System;
using System.Collections.Generic;

namespace TVPrograms.Data
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public short? Age { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

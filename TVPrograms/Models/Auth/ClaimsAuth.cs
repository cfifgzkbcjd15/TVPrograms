using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPrograms.Models.Auth
{
    public class ClaimsAuth
    {
        public string Role { get; set; }
        public string Sex { get; set; }
        public short Age { get; set; }
        public Guid Id { get; set; }
    }
}

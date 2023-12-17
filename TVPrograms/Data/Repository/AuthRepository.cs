using TVPrograms.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public async Task<ClaimsAuth> CheckUser(LoginModel model)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Users.Where(x => x.Login == model.Email && x.Password == model.Password)
                    .Select(x => new ClaimsAuth { Id = x.Id, Sex = x.Sex, Age = (short)x.Age })
                    .FirstOrDefaultAsync();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TVPrograms.Models.Users;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public async Task<ResponseUser> GetUserById(Guid userId)
        {
            using (LimeHdTvContext db = new LimeHdTvContext(options))
            {
                return await db.Users.Where(x => x.Id == userId).Select(x => new ResponseUser { Login = x.Login, Age = (short)x.Age, Gender = x.Sex }).FirstOrDefaultAsync();
            }
        }
    }
}

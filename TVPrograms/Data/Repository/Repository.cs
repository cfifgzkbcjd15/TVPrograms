using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TVPrograms.Data.Repository
{
    public partial class Repository
    {
        public DbContextOptions<LimeHdTvContext> options { get; set; }
        public Repository(IConfiguration conf)
        {
            options = new DbContextOptionsBuilder<LimeHdTvContext>().UseNpgsql(conf.GetConnectionString("DefaultConnection")).Options;
        }
    }
}

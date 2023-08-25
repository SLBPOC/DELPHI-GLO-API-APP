using Microsoft.EntityFrameworkCore;
using Delfi.Glo.Entities.Db;

namespace Delfi.Glo.PostgreSql.Dal
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Crew> Crew { get; set; }
        public DbSet<WellGeneralInfo> GeneralInfo { get; set; }

        public DbSet<Well> Well { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<CustomAlert> CustomAlerts { get; set; }
        public DbSet<Alerts> Alerts { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}

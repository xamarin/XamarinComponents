using Microsoft.EntityFrameworkCore;
using NugetAuditor.Core.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace NugetAuditor.Core
{
    public class AuditorDbContext : DbContext
    {

        public DbSet<ProcessResult> Results { get; set; }

        public AuditorDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conf = string.Empty;

            if (SettingsHelper.ConfigurationProvider != null)
            {
                conf = SettingsHelper.ConfigurationProvider.GetConnectionString("dbConn");
            }
            else
            {
                conf = "Server=.;Database=NugetAuditor;User Id = development; Password = password;Trusted_Connection=False;MultipleActiveResultSets=true";
            }
            

            optionsBuilder.UseSqlServer(conf);

           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProcessResult>().HasKey(x => x.PackageId);
        }

        public static async Task InitializeAsync()
        {
            var aDb = new AuditorDbContext();

            var migrations = await aDb.Database.GetPendingMigrationsAsync();

            if (migrations.Count() > 0)
            {
                await aDb.Database.MigrateAsync();
            }
        }

    }
}

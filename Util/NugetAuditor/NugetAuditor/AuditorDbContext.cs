using Microsoft.EntityFrameworkCore;
using NugetAuditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetAuditor
{
    public class AuditorDbContext : DbContext
    {

        public DbSet<ProcessResult> Results { get; set; }

        public AuditorDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conf = System.Configuration.ConfigurationManager.ConnectionStrings["dbConn"];

            optionsBuilder.UseSqlServer(conf.ConnectionString);

           
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

using DOMAIN.Helpers;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Context
{
    public class SqlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=DESKTOP-J0S4R9L;Database=miniOdev;Trusted_Connection=true");
        }

        public DbSet<HastaBilgileri> HastaBilgileri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DbInitializer(modelBuilder).Configure();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<byte[]>()
                .HaveConversion<byte[]>()
                .HaveMaxLength(255);
        }
    }
}

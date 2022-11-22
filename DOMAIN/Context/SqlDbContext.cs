using DOMAIN.Helpers;
using DOMAIN.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Context
{
    public class SqlDbContext : IdentityDbContext<CustomUser>
    {
        public SqlDbContext()
        {
                
        }
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=94.73.170.25;Database=u0800400_minOdev;User Id=u0800400_odev;Password=MiniOdev34.....;");
        }

        public DbSet<HastaBilgileri> HastaBilgileri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new DbInitializer(modelBuilder).Configure();
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<byte[]>()
                .HaveConversion<byte[]>()
                .HaveMaxLength(255);
        }
    }
}

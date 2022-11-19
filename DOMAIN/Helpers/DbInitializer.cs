using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Helpers
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Configure()
        {
            modelBuilder.Entity<HastaBilgileri>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HastaBilgileri>().Property(x => x.Ad).IsRequired();
            modelBuilder.Entity<HastaBilgileri>().Property(x => x.Soyad).IsRequired();
            modelBuilder.Entity<HastaBilgileri>().Property(x => x.Cinsiyet).IsRequired();
            modelBuilder.Entity<HastaBilgileri>().Property(x => x.KanGrubu).IsRequired();
        }
    }

}

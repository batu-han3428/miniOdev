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

            modelBuilder.Entity<JobTable>()
               .HasQueryFilter(p => p.IS_ACTIVE == true);

            modelBuilder.Entity<JobTable>()
              .HasOne<JobType>(s => s.JobType)
              .WithMany(g => g.jobTable);

            modelBuilder.Entity<JobTable>()
              .HasOne<CustomUser>(s => s.CustomUser)
              .WithMany(g => g.jobTable);

            modelBuilder.Entity<JobType>().HasData(
               new JobType { ID_JOB_TYPE = 1, JOB_TYPE_NAME = "Günlük" },
               new JobType { ID_JOB_TYPE = 2, JOB_TYPE_NAME = "Haftalık" },
               new JobType { ID_JOB_TYPE = 3, JOB_TYPE_NAME = "Aylık" }
           );

            modelBuilder.Entity<VisitorInformation>()
             .HasOne<VisitorInformationAsn>(s => s.Asn)
             .WithMany(g => g.VisitorInformation);


            modelBuilder.Entity<VisitorInformation>()
             .HasOne<VisitorInformationThreat>(s => s.Threat)
             .WithMany(g => g.VisitorInformation);
        }
    }

}

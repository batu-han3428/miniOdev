using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class JobRepository : BaseRepository<DOMAIN.Models.JobTable>, IJobRepository
    {
        public List<DOMAIN.Models.JobTable> JobBilgileriniGetir()
        {
            return context.Set<DOMAIN.Models.JobTable>().Include(x => x.JobType).Where(x => x.IS_ACTIVE == true).ToList();
        }
       
    }
}

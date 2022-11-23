using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public interface IJobRepository : IBaseRepository<DOMAIN.Models.JobTable>
    {
        List<DOMAIN.Models.JobTable> JobBilgileriniGetir();
    }
}

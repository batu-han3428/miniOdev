using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    public class JobServices : IJobServices
    {
        private readonly IJobRepository _JobRepository;
        public JobServices(IJobRepository JobRepository)
        {
            _JobRepository = JobRepository;
        }

        public List<DOMAIN.Models.JobTable> JobBilgileriniGoruntule()
        {
            return _JobRepository.JobBilgileriniGetir();
        }
    }
}

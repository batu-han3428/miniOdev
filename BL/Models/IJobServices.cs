using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public interface IJobServices
    {
        List<DOMAIN.Models.JobTable> JobBilgileriniGoruntule();
    }
}

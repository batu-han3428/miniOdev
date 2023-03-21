using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public interface IVisitorInformationRepository : IBaseRepository<DOMAIN.Models.VisitorInformation>
    {
        bool GetVisitorInformation(string ipAddress);
        List<string?> GetBlackList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public interface IVisitorInformationServices
    {
        int SetVisitorInformation(DOMAIN.Models.VisitorInformation visitorInformation);
        bool GetVisitorInformation(string ipAddress);
        List<string?> GetBlackList();
    }
}

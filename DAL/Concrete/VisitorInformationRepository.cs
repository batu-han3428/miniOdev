using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class VisitorInformationRepository : BaseRepository<DOMAIN.Models.VisitorInformation>, IVisitorInformationRepository
    {
        public List<string?> GetBlackList()
        {
            return context.VisitorInformation.Where(x => x.CallingCode != "90" || x.Threat.IsThreat == true).Select(x=>x.Ip).ToList();
        }

        public bool GetVisitorInformation(string ipAddress)
        {
            return context.VisitorInformation.Any(x=>x.Ip == ipAddress && x.Process == "GET" && x.Time.Date == DateTime.Now.Date);
        }
    }
}

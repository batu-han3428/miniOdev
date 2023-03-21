using BL.Models;
using DAL.Models;
using DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    public class VisitorInformationServices : IVisitorInformationServices
    {
        private readonly IVisitorInformationRepository _visitorInformationRepository;
        public VisitorInformationServices(IVisitorInformationRepository VisitorInformationRepository)
        {
            _visitorInformationRepository = VisitorInformationRepository;
        }

        public List<string?> GetBlackList()
        {
            return _visitorInformationRepository.GetBlackList();
        }

        public bool GetVisitorInformation(string ipAddress)
        {
            return _visitorInformationRepository.GetVisitorInformation(ipAddress);
        }

        public int SetVisitorInformation(VisitorInformation visitorInformation)
        {
            return _visitorInformationRepository.Add(visitorInformation);
        }
    }
}

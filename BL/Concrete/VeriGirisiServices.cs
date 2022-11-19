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
    public class VeriGirisiServices: IVeriGirisiServices
    {
        private readonly IVeriGirisiRepository _veriGirisiRepository;
        public VeriGirisiServices(IVeriGirisiRepository VeriGirisiRepository)
        {
            _veriGirisiRepository = VeriGirisiRepository;
        }

        public int HastaBilgileriKaydet(DOMAIN.Models.HastaBilgileri hastaBilgileri)
        {
            return _veriGirisiRepository.Add(hastaBilgileri);
        }

        public List<DOMAIN.Models.HastaBilgileri> HastaBilgileriGoruntule(Expression<Func<DOMAIN.Models.HastaBilgileri, bool>> filter = null)
        {
            return _veriGirisiRepository.AllList(filter);
        }
    }
}

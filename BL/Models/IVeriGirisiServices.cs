using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public interface IVeriGirisiServices
    {
        int HastaBilgileriKaydet(DOMAIN.Models.HastaBilgileri hastaBilgileri);
        List<DOMAIN.Models.HastaBilgileri> HastaBilgileriGoruntule(Expression<Func<DOMAIN.Models.HastaBilgileri, bool>> filter = null);
    }
}

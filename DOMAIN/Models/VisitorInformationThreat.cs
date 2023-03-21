using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models
{
    public class VisitorInformationThreat
    {
        [Key]
        public int Id { get; set; }
        public bool? IsTor { get; set; }

        public bool? IsProxy { get; set; }

        public bool? IsAnonymous { get; set; }

        public bool? IsKnownAttacker { get; set; }

        public bool? IsKnownAbuser { get; set; }

        public bool? IsThreat { get; set; }

        public bool? IsBogon { get; set; }
        public List<VisitorInformation> VisitorInformation { get; set; }
    }
}

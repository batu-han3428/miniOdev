using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models
{
    public class VisitorInformationAsn
    {
        [Key]
        public int Id { get; set; }
        public string? Asn { get; set; }

        public string? Name { get; set; }

        public string? Domain { get; set; }

        public string? Route { get; set; }

        public string? Type { get; set; }
        public List<VisitorInformation> VisitorInformation { get; set; }
    }
}

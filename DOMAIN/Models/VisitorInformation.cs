using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models
{
    public class VisitorInformation
    {
        [Key]
        public int Id { get; set; }
        public string? Ip { get; set; }
        public DateTime Time{ get; set; }
        public string Process { get; set; }
        public string Path { get; set; }
        public bool? IsEu { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? RegionCode { get; set; }

        public string? CountryName { get; set; }

        public string? CountryCode { get; set; }

        public string? ContinentName { get; set; }

        public string? ContinentCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
        public int AsnId { get; set; }
        public virtual VisitorInformationAsn Asn { get; set; }

        public string? Organisation { get; set; }

        public string? Postal { get; set; }

        public string? CallingCode { get; set; }

        public Uri? Flag { get; set; }

        public string? EmojiFlag { get; set; }

        public string? EmojiUnicode { get; set; }
        public int ThreatId { get; set; }
        public virtual VisitorInformationThreat Threat { get; set; }

        public int? Count { get; set; }
    }
}

using Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class VeriGirisiDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string Ad { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Soyad { get; set; }
        [Required]
        [EnumDataType(typeof(Enums.Cinsiyet))]
        public Enums.Cinsiyet Cinsiyet { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string KanGrubu { get; set; }
    }
}

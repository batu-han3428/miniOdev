using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public static class Enums
    {   
        public enum Cinsiyet
        {
            [Display(Name = "Kadın")]
            Female = 1,
            [Display(Name = "Erkek")]
            Male = 2,
            [Display(Name = "Diğer")]
            Other = 3
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models
{
    public class JobTable
    {
        [Key]
        public int ID_JOB { get; set; }
        //public int ID_JOB_TYPE { get; set; }
        public JobType JobType { get; set; }
        public string JOB_KEY { get; set; }
        public TimeSpan JOB_TIME { get; set; }
        public int? DAY { get; set; }
        public Boolean IS_ACTIVE { get; set; }
        public string? DESCRIPTION { get; set; }
    }
}

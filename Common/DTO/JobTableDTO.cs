using DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class JobTableDTO
    {
        public int JobTypeId { get; set; }
        //public virtual JobType JobType { get; set; }
        public string? JOB_KEY { get; set; }
        public TimeSpan JOB_TIME { get; set; }
        public int? DAY { get; set; }
        public Boolean? IS_ACTIVE { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? CustomUserId { get; set; }
        //public virtual CustomUser CustomUser { get; set; }

    }
}

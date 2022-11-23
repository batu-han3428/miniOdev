using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models
{
    public class JobType
    {
        [Key]
        public int ID_JOB_TYPE { get; set; }
        public string JOB_TYPE_NAME { get; set; }
        public List<JobTable> jobTable { get; set; }
    }
}

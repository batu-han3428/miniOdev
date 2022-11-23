using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Entities
{
    public class SmtpConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public NetworkCredential Credentials{ get; set; }
        //public string User { get; set; }
        //public string Password { get; set; }
        //public bool UseSsl { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
    }
}

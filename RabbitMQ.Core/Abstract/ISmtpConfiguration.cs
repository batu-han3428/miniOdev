using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Abstract
{
    public interface ISmtpConfiguration
    {
        string Host { get; }
        int Port { get; }
        //string User { get; }
        //string Password { get; }
        //bool UseSSL { get; }
        NetworkCredential Credentials { get; }    
        bool EnableSsl { get; }

        bool UseDefaultCredentials { get; }
        SmtpDeliveryMethod DeliveryMethod { get; }
        SmtpConfig GetSmtpConfig();
    }
}

using Microsoft.Extensions.Configuration;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Concrete
{
    public class SmtpConfiguration:ISmtpConfiguration
    {
        private IConfiguration Configuration { get; }

        public SmtpConfiguration(IConfiguration configuration) => Configuration = configuration;

        public string Host => Configuration.GetSection("SmtpConfig:Host").Value;
        public int Port => Convert.ToInt32(Configuration.GetSection("SmtpConfig:Port").Value);
        //public string User => Configuration.GetSection("SmtpConfig:User").Value;
        //public string Password => Configuration.GetSection("SmtpConfig:Password").Value;
        public bool EnableSsl => Convert.ToBoolean(Configuration.GetSection("SmtpConfig:EnableSsl").Value);
        public NetworkCredential Credentials => new NetworkCredential(Configuration.GetSection("SmtpConfig:User").Value, Configuration.GetSection("SmtpConfig:Password").Value);
        public bool UseDefaultCredentials => Convert.ToBoolean(Configuration.GetSection("SmtpConfig:UseDefaultCredentials").Value);
        public SmtpDeliveryMethod DeliveryMethod => SmtpDeliveryMethod.Network;


        public SmtpConfig GetSmtpConfig()
        {
            return new SmtpConfig
            {
                Host = Host,
                Credentials = Credentials,
                Port = Port,
                EnableSsl = EnableSsl,
                UseDefaultCredentials = UseDefaultCredentials,
                DeliveryMethod = DeliveryMethod
            };
        }
    }
}

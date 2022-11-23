using Microsoft.Extensions.Configuration;
using RabbitMQ.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Concrete
{
    public class RabbitMQConfiguration : IRabbitMQConfiguration
    {
        public IConfiguration Configuration { get; }
        public RabbitMQConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string HostName => Configuration.GetSection("RabbitMQConfiguration:HostName").Value;

        public string UserName => Configuration.GetSection("RabbitMQConfiguration:UserName").Value;

        public string Password => Configuration.GetSection("RabbitMQConfiguration:Password").Value;
    }
}

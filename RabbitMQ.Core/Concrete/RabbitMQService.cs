using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Concrete
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IRabbitMQConfiguration _rabbitMQConfiguration;
        public RabbitMQService(IRabbitMQConfiguration rabbitMQConfiguration)
        {
            _rabbitMQConfiguration = rabbitMQConfiguration ?? throw new ArgumentNullException(nameof(rabbitMQConfiguration));
        }

        public IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    //HostName = _rabbitMQConfiguration.HostName,
                    //UserName = _rabbitMQConfiguration.UserName,
                    //Password = _rabbitMQConfiguration.Password,
                    Uri= new Uri("amqps://tgdxkgbs:OBlmdA0t35ytzCwkH3GsTPqKhwm-luar@rat.rmq2.cloudamqp.com/tgdxkgbs")
                };
           
                //Otomatik bağlantı kurtarmayı etkinleştirmek için,
                factory.AutomaticRecoveryEnabled = true;
                //Her 10 sn. de bir tekrar bağlantı toparlamaya çalışır
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
                //sunucudan bağlantısı kesildikten sonra kuyruktaki mesaj tüketimini sürdürmez
                factory.TopologyRecoveryEnabled = false;

                return factory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                //loglama işlemi burada yapılabilir
                Thread.Sleep(5000);

                return GetConnection();
            }
        }

        public IModel GetModel(IConnection connection)
        {
            return connection.CreateModel();
        }
    }
}

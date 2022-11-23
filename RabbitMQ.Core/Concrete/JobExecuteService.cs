using Microsoft.Extensions.Configuration;
using Quartz;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Consts;
using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Concrete
{
    public class JobExecuteService: IJob
    {
        private readonly IPublisherService _publisherService;
        private IConfiguration Configuration { get; }
        public JobExecuteService(IPublisherService PublisherService, IConfiguration configuration)
        {
            _publisherService = PublisherService;
            Configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            try
            {
                Task taskA = new Task(() => Console.WriteLine("Hello from task at {0}", DateTime.Now.ToString()));
                taskA.Start();
                await Console.Out.WriteLineAsync("HelloJob is executing.");
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(context));
            }
        }

        public void Job1()
        {
            // _publisherService.Enqueue(
            //PrepareMessages(),
            //RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString()
            //);
            Console.WriteLine("job1 çalıştı");
        }
        public void Job2()
        {
            _publisherService.Enqueue(
            PrepareMessages(),
            RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString()
            );
        }
        public void Job3()
        {
            _publisherService.Enqueue(
            PrepareMessages(),
            RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString()
            );
        }
        public void Job4()
        {
            _publisherService.Enqueue(
           PrepareMessages(),
           RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString()
           );
        }


        private IEnumerable<MailMessageData> PrepareMessages()
        {

            var messages = new List<MailMessageData>();

            messages.Add(new MailMessageData()
            {
                To = "batu_6407@hotmail.com.tr",
                From = Configuration.GetSection("SmtpConfig:User").Value,
                Subject = "Mail Gönderimi",
                Body = "Mail gönderimi başarılı"
            });

            return messages;
        }
        //public class Job : IJob
        //{
           
           

        //}
    }
}

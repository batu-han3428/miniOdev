using DOMAIN.Models;
using Quartz;
using Quartz.Spi;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Concrete;
using RabbitMQ.Core.Consts;
using RabbitMQ.Core.Entities;
using System.Net.Mail;
using System.Reflection;

namespace miniOdev.Helpers
{
    public class JobExecuteService : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobExecuteService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<RealJob>();
        }

        public void ReturnJob(IJob job)
        {
         
        }

        public class RealJob : IJob
        {
            private readonly IPublisherService _publisherService;
            private IConfiguration Configuration { get; }
            public RealJob(IPublisherService PublisherService, IConfiguration configuration)
            {
                _publisherService = PublisherService;
                Configuration = configuration;
            }

            public async Task Execute(IJobExecutionContext context)
            {

                try
                {

                    await Console.Out.WriteLineAsync(context.JobDetail.Key.Name);
                    //MethodInfo method = typeof(RealJob).GetMethod(context.JobDetail.Key.Name);


                    await Console.Out.WriteLineAsync("execute calıstı");
                    //if (method != null)
                    //{
                        await Console.Out.WriteLineAsync("method null değil");

                    //var r = method.Invoke(this, new object[] { context.JobDetail.JobDataMap});
                    Job1(context.JobDetail.JobDataMap);
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public void Job1(JobDataMap jobDataMapObject)
            {
                _publisherService.Enqueue(
                      PrepareMessages(jobDataMapObject.Values),
                    RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString()
                );
                Console.WriteLine("job1 çalıştı");
            }

            private IEnumerable<MailMessageData> PrepareMessages(ICollection<object> userDataObject)
            {
                var userData = userDataObject.OfType<JobTable>().FirstOrDefault();

                var messages = new List<MailMessageData>();

                messages.Add(new MailMessageData()
                {
                    To = userData?.CustomUser.Email ?? "batu_6407@hotmail.com.tr",
                    From = Configuration.GetSection("SmtpConfig:User").Value,
                    Subject = "Hasta Bilgileri",
                    Body = $"Merhaba {userData?.CustomUser.UserName}. Hasta bilgileri ektedir. Saygılarımızla."
                });

                return messages;
            }

        }
    }
}

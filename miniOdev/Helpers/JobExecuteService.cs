using Quartz;
using Quartz.Spi;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Concrete;
using RabbitMQ.Core.Consts;
using RabbitMQ.Core.Entities;
using System.Reflection;

namespace miniOdev.Helpers
{
    public class JobExecuteService : /*IJob*/IJobFactory
    {
        //private readonly IPublisherService _publisherService;
        //private IConfiguration Configuration { get; }
        //public JobExecuteService(IPublisherService PublisherService, IConfiguration configuration)
        //{
        //    _publisherService = PublisherService;
        //    Configuration = configuration;
        //}
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


        //private IEnumerable<MailMessageData> PrepareMessages()
        //{

        //    var messages = new List<MailMessageData>();

        //    messages.Add(new MailMessageData()
        //    {
        //        To = "batu_6407@hotmail.com.tr",
        //        From = Configuration.GetSection("SmtpConfig:User").Value,
        //        Subject = "Mail Gönderimi",
        //        Body = "Mail gönderimi başarılı"
        //    });

        //    return messages;
        //}
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

                //try
                //{
                //    Task taskA = new Task(() => Console.WriteLine("Hello from task at {0}", DateTime.Now.ToString()));
                //    taskA.Start();
                //    await Console.Out.WriteLineAsync("HelloJob is executing.");
                //}
                //catch (Exception ex)
                //{
                //    throw new ArgumentNullException(nameof(context));
                //}
                //bool result = false;
                try
                {

                    await Console.Out.WriteLineAsync(context.JobDetail.Key.Name);
                    MethodInfo method = typeof(RealJob).GetMethod(context.JobDetail.Key.Name);


                    await Console.Out.WriteLineAsync("execute calıstı");
                    if (method != null)
                    {
                        await Console.Out.WriteLineAsync("method null değil");

                        //result = true;

                        var r = method.Invoke(this, new object[] { context.JobDetail.JobDataMap });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //return Task.FromResult(result);
            }

            public void Job1(JobDataMap data)
            {
                var veri = data.Values;
                _publisherService.Enqueue(
               PrepareMessages(),
               RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString()
               );
                Console.WriteLine("job1 çalıştı");
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
        }
    }
}

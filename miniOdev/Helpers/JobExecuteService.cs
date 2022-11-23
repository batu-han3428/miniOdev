using Quartz;

namespace miniOdev.Helpers
{
    public class JobExecuteService : IJob
    {
        //private readonly IPublisherService _publisherService;
        //private IConfiguration Configuration { get; }
        //public JobExecuteService(IPublisherService PublisherService, IConfiguration configuration)
        //{
        //    _publisherService = PublisherService;
        //    Configuration = configuration;
        //}

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
    }
}

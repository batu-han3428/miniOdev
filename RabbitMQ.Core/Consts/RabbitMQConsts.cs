using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Consts
{
    public class RabbitMQConsts
    {
        //yaşam süresi
        public static int MessagesTTL { get; set; } = 1000 * 60 * 60 * 2;

        //aynı anda eş zamanlı e-posta gönderimi sayısı, thread açma için sınırı belirleriz
        public static ushort ParallelThreadsCount { get; set; } = 3;
        public enum RabbitMqConstsList
        {
            [Description("QueueNameEmail")]
            QueueNameEmail = 1,
            [Description("QueueNameSms")]
            QueueNameSms = 2
        }
    }
}

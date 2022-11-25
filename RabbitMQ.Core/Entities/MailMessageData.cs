using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Entities
{
    public class MailMessageData
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Attachment Attachment { get; set; }

        public MailMessage GetMailMessage(byte[] excelData)
        {
            var mailMessage = new MailMessage
            {
                Subject = this.Subject,
                Body = this.Body,
                From = new MailAddress(this.From)
            };
            mailMessage.To.Add(To);
            mailMessage.Attachments.Add(new Attachment(new MemoryStream(excelData), "HastaBilgileri.xlsx", "application/vnd.ms-excel"));
            return mailMessage;
        }
    }
}

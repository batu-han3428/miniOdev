using BL.Models;
using RabbitMQ.Core.Abstract;
using RabbitMQ.Core.Consts;
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
    public class MailSender: IMailSender
    {
        private readonly ISmtpConfiguration _smtpConfiguration;
        private readonly IVeriGirisiServices _veriGirisiServices;
        public MailSender(ISmtpConfiguration smtpConfiguration, IVeriGirisiServices VeriGirisiServices)
        {
            _smtpConfiguration = smtpConfiguration;
            _veriGirisiServices = VeriGirisiServices;
        }

        public async Task<MailSendResult> SendMailAsync(MailMessageData emailMessage)
        {
            Console.WriteLine($"EmailRabbitMQProcessor SendMailAsync method  => Calısma zamanı: {DateTime.Now.ToShortTimeString()}");

            MailSendResult result;
            byte[] excelData = ExcelManager.ExcelOlustur(_veriGirisiServices.HastaBilgileriGoruntule());
            MailMessage mailMessage = emailMessage.GetMailMessage(excelData);

            try
            {
                var smtpConfig = _smtpConfiguration.GetSmtpConfig();
                SmtpClient smtpClient = new SmtpClient(smtpConfig.Host, smtpConfig.Port);         
                smtpClient.Credentials = smtpConfig.Credentials;
                smtpClient.EnableSsl = /*smtpConfig.EnableSsl;*/ true;
                smtpClient.UseDefaultCredentials = smtpConfig.UseDefaultCredentials;
                smtpClient.DeliveryMethod = smtpConfig.DeliveryMethod;

                using (var client = smtpClient/*CreateSmtpClient(*//*)*/)
                {
                    await client.SendMailAsync(mailMessage);
                    string resultMessage = $"donus mesajı metni  {string.Join(",", mailMessage.To)}.";
                    result = new MailSendResult(mailMessage, true, resultMessage);
                    Console.WriteLine($"EmailRabbitMQProcessor running => resultMessage to:{mailMessage.To}");
                }
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                result = new MailSendResult(mailMessage, false, $"Hata: {ex.Message}");
            }
            finally
            {
                Thread.Sleep(MailConsts.SendTimeout);
            }
            return result;
        }
    }
}

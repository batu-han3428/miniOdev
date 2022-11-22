using System.Net.Mail;

namespace miniOdev.Helpers
{
    public class EmailHelper
    {
        public bool SendEmail(string email, string mesaj)
        {
            #region MailMessage
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("bticaret01@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "Üyelik Onaylama";
            mailMessage.Body = mesaj;
            mailMessage.IsBodyHtml = true;
            #endregion

            #region Smtp Ayarlari
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("bticaret01@gmail.com", "knkxxoqdmkdiliaz");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            #endregion
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
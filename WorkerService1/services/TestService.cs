using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService1.services
{
   public class TestService
    {
        public static void SendEmail(String ToEmail, String Subj, string Message)
        {
            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 200000;
                MailMessage MailMsg = new MailMessage();
                System.Net.Mime.ContentType HTMLType = new System.Net.Mime.ContentType("text/html");

                string strBody = "This is a test mail.";

                MailMsg.BodyEncoding = System.Text.Encoding.Default;
                MailMsg.To.Add(ToEmail);
                MailMsg.Priority = System.Net.Mail.MailPriority.High;
                MailMsg.Subject = "Subject - Window Service";
                MailMsg.Body = strBody;
                MailMsg.IsBodyHtml = true;
                System.Net.Mail.AlternateView HTMLView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(strBody, HTMLType);

                smtpClient.Send(MailMsg);
                WriteErrorLog("Mail sent successfully!");
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.InnerException.Message);
                throw;
            }
        }

        private static void WriteErrorLog(string v)
        {
            throw new NotImplementedException();
        }
    }
}

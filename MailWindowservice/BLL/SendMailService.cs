using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailWindowservice.BLL
{
    class SendMailService
    {
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
        // This function write Message to log file.
        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
        // This function contains the logic to send mail.
        public static void SendEmail(String ToEmail, String Subj, string Message)
        {
            SampleTestEntities Entity = new SampleTestEntities();
            var emailList = Entity.Student_Personal_Details.Where(x=>x.Is_Deleted==false).ToList();
            foreach(var value in emailList)
            {
                string name = value.First_Name.ToString();
                string subject = value.Subject.ToString();
                try
            { 
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 200000;
                MailMessage MailMsg = new MailMessage();
                System.Net.Mime.ContentType HTMLType = new System.Net.Mime.ContentType("text/html");

                    string strBody = string.Format("Hi{0}You Have Test in {1}", name,subject);

                    MailMsg.BodyEncoding = System.Text.Encoding.Default;
                MailMsg.To.Add(value.Email_Id);
                MailMsg.Priority = System.Net.Mail.MailPriority.High;
                MailMsg.Subject = "Test Schedule";
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
        }
    }
}

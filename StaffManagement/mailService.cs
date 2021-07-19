using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace StaffManagement
{
    public class mailService

    {
        private readonly string from = "sangavi.palanisamy@dotnetethics.com";
        private readonly string password = "10-Jun-99";

        [Obsolete]
        public void sendMessage(string to,string subject,string bodyText)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = bodyText

            };
            using(var client=new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com");
                client.Authenticate(from, password);
                client.Send(message);
                client.Disconnect(true);
            }
         
        }
       

    }
}

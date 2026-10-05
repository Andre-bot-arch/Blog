using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class EmailService
    {
        public bool Send(string toName, string toEmail, string subject, string body, string fromName = "andre", string fromEmail = "andredev808@gmail.com")
        {
            var smtp = new SmtpClient(Configuration.Smtp.Host, Convert.ToInt32(Configuration.Smtp.Port));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);

            var mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
        }
    }
}
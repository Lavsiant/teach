using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TeachMe.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {


            MailAddress from = new MailAddress("Lavsiant0@gmail.com", "lavsiain");

            MailAddress to = new MailAddress(email);

            MailMessage m = new MailMessage(from, to);

            m.Subject = subject;

            m.Body = message;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);

            smtp.Credentials = new NetworkCredential("Lavsiant0@gmail.com", "(xp+200)_5*");
            smtp.EnableSsl = true;
            smtp.Send(m);

           
        }
    }
}

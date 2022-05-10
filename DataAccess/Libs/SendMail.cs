using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Libs
{
    public class SendMail
    {
        public static int SendMailToAccount(string body,string email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("buingochuy124@gmail.com");
            mail.Subject = "Verify Email";
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("buingochuy124@gmail.com", "su24122000"); // Enter seders User name and password       
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return 1;
        }

    }
}

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DotNetEnv;
namespace CRM.Services
{
    public class MailService : IMailService
    {
        public async Task SendMail(string userEmail, string subject, string body)
        {
            Env.Load();

            string mail = Env.GetString("MAIL_LOGIN");
            string password = Env.GetString("MAIL_PASSWORD");
            string smtpHost = Env.GetString("MAIL_SMTP");

            using var smtpClient = new SmtpClient(smtpHost)
            {
                Port = 587, 
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true
            };

            using var message = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            message.To.Add(new MailAddress(userEmail));

            await smtpClient.SendMailAsync(message);
        }
    }
}
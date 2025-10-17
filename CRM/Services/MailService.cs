using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DotNetEnv;
namespace CRM.Services
{
    public class MailService : IMailService
    {
        private readonly string smtpHost;
        private readonly string mail;
        private readonly string password;
        private readonly int port;
        public MailService(IConfiguration config)
        {
            smtpHost = config["MAIL_SMTP"];
            mail = config["MAIL_LOGIN"];
            password = config["MAIL_PASSWORD"];
            port = Convert.ToInt32(config["MAIL_PORT"]);
        }
        
        public async Task SendMail(string userEmail, string subject, string body)
        {
            
            using var smtpClient = new SmtpClient(smtpHost)
            {
                Port = port, 
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
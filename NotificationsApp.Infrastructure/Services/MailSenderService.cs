using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using NotificationsApp.Domain.ServicesContract;
using System;
using System.Threading.Tasks;

namespace NotificationsApp.Infrastructure.Services
{
    public class MailSenderService : IMailSenderService
    {
        private readonly ILogger<MailSenderService> _logger;

        public MailSenderService(ILogger<MailSenderService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// отпрвка сообщения по почте
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="mailTo"></param>
        /// <returns></returns>
        public async Task SendMailMessage(string subject, string message, string mailTo)
        {
            try
            {
                var email = new MimeMessage();
               
                email.From.Add(new MailboxAddress("System Notif", "horina.vika@mail.ru"));
                email.To.Add(new MailboxAddress("user", mailTo));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = $"<p>{message}</p>" };

                using var client = new SmtpClient();
                client.Connect("smtp.mail.ru", 465, true);
                
                client.Authenticate("horina.vika@mail.ru", "7I1do5FROM_plenty3");

                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                throw;
            }
        }
    }
}

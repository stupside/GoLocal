using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Mailing.Commons.Configurations;
using GoLocal.Shared.Mailing.Commons.Models;
using GoLocal.Shared.Mailing.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace GoLocal.Shared.Mailing.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;

        public EmailService(IOptions<EmailConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_configuration.SmtpUsername, _configuration.SmtpEmail));
            
            foreach (string recipient in message.Recipients)
                mail.To.Add(new MailboxAddress(message.UserName, recipient));
            
            mail.Subject = message.Object;

            mail.Body = new TextPart(TextFormat.Plain)
            {
                Text = message.Content
            };

            var client = new SmtpClient();
            await client.ConnectAsync(_configuration.SmtpServer, _configuration.SmtpPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable,cancellationToken);
            
            await client.AuthenticateAsync(_configuration.SmtpUsername, _configuration.SmtpPassword, cancellationToken);

            await client.SendAsync(mail, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
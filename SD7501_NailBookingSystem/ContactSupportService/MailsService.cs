using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SD7501_NailBookingSystem.Models;
using System.IO;  
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.ContactSupportService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
    public class MailsService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public MailsService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.EmailAddress);
            email.To.Add(MailboxAddress.Parse(emailRequest.EmailTo));
            email.Subject = emailRequest.Enquiry;
            var builder = new BodyBuilder();
            if (emailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in emailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = emailRequest.Description;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.ServerHost, _emailSettings.ServerPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.EmailAddress, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}

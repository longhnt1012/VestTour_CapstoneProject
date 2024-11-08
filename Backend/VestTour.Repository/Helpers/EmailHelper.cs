using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Configuration;
using VestTour.Repository.Models;
using System.Threading;

namespace VestTour.Repository.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly EmailConfig _emailConfig;


        public EmailHelper(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;

        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            using var smtp = new SmtpClient(_emailConfig.Provider, _emailConfig.Port)
            {
                Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password),
                EnableSsl = true
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.DefaultSender),
                Subject = emailRequest.Subject,
                Body = emailRequest.Content
            };
            mailMessage.To.Add(emailRequest.To);

            // Add attachments if provided
            if (emailRequest.AttachFilePatch?.Length > 0)
            {
                foreach (var path in emailRequest.AttachFilePatch)
                {
                    var attachment = new Attachment(path);
                    mailMessage.Attachments.Add(attachment);
                }
            }

            try
            {
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Consider adding a logging mechanism here to capture the exception details.
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
        public async Task CustomerSendContactEmail(ContactRequest contactRequest)
        {
            using var smtp = new SmtpClient(_emailConfig.Provider, _emailConfig.Port)
            {
                Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password),
                EnableSsl = true
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.DefaultSender, $"Contact from {contactRequest.Name}"), // Display customer's name
                Subject = $"Contact Form: {contactRequest.Subject}",
                Body = $@"New contact request from:
                  Name: {contactRequest.Name}
                  Email: {contactRequest.Email}
                  Message: {contactRequest.Message}",
                IsBodyHtml = false
            };

            // Set the "To" address to your email
            mailMessage.To.Add(_emailConfig.DefaultSender);

            // Set the "Reply-To" address to the customer's email
            mailMessage.ReplyToList.Add(new MailAddress(contactRequest.Email));

            try
            {
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send contact email", ex);
            }
        }




    }
}
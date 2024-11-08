using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Configuration;
using VestTour.Repository.Helpers;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IEmailHelper _emailHelper;
        private readonly EmailConfig _emailConfig;

        public ContactService(IEmailHelper emailHelper, IOptions<EmailConfig> emailConfig)
        {
            _emailHelper = emailHelper;
            _emailConfig = emailConfig.Value;
        }

        public async Task SendContactEmailAsync(ContactRequest contactRequest)
        {
            // Compose the email to be sent to the owner's email
            var emailRequest = new EmailRequest
            {
                To = _emailConfig.DefaultSender, // The owner's email
                Subject = $"Contact Form: {contactRequest.Subject}",
                Content = $@"New contact request from:
                Name:{contactRequest.Name}
                Email:{contactRequest.Email}
                Message:{contactRequest.Message}"
            };

            // Send the email
            await _emailHelper.SendEmailAsync(emailRequest);
        }
    }

}

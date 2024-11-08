using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Helpers
{
    public interface IEmailHelper
    {
        public Task SendEmailAsync(EmailRequest emailRequest);
        public Task CustomerSendContactEmail(ContactRequest contactRequest);


    }
}

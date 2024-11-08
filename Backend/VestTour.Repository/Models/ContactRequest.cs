using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class ContactRequest
    {
        public string Name { get; set; }         // Customer's name
        public string Email { get; set; }        // Customer's email
        public string Subject { get; set; }      // Subject of the message
        public string Message { get; set; }      // The actual message content
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Repository.Models
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
       // public bool IsHtml { get; set; }
        public string Content { get; set; }
        public string ReplyTo { get; set; }
        public string[] AttachFilePatch { get; set; } = Array.Empty<string>();
    }
}
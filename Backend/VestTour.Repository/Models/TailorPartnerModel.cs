using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;

namespace VestTour.Repository.Models
{
    public class TailorPartnerModel
    {
        public int TailorPartnerId { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public string? Location { get; set; }
        public string? Status { get; set; }
    }
}

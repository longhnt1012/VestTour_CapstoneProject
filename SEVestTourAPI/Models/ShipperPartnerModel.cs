﻿namespace VestTour.Repository.Models
{
    public class ShipperPartnerModel
    {
        public int ShipperPartnerId { get; set; }          // Corresponds to ShipperPartnerID in the database
        public string ShipperPartnerName { get; set; }     // Name of the shipper partner
        public string? Phone { get; set; }                  // Phone number
        public string? Company { get; set; }                // Company name
        public string? Status { get; set; }                 // Status
    }
}
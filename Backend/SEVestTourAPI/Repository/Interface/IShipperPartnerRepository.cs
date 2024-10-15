using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Repository.Interface
{
    public interface IShipperPartnerRepository
    {
        Task<List<ShipperPartnerModel>> GetAllShipperPartnersAsync();     // Get all shipper partners
        Task<ShipperPartnerModel?> GetShipperPartnerByIdAsync(int id);   // Get shipper partner by ID
        Task<int> AddShipperPartnerAsync(ShipperPartnerModel shipperPartner); // Add a new shipper partner
        Task UpdateShipperPartnerAsync(int id, ShipperPartnerModel shipperPartner); // Update an existing shipper partner
        Task DeleteShipperPartnerAsync(int id);                            // Delete a shipper partner by ID
    }
}

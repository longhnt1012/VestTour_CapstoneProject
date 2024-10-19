using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Interface
{
    public interface IShipperPartnerRepository
    {
        Task<List<ShipperPartnerModel>> GetAllShipperPartnersAsync();     // Get all shipper partners
        Task<ShipperPartnerModel?> GetShipperPartnerByIdAsync(int id);   // Get shipper partner by ID
        Task<int> AddShipperPartnerAsync(ShipperPartnerModel shipperPartner); // Add a new shipper partner
        Task UpdateShipperPartnerAsync(int id, ShipperPartnerModel shipperPartner); // Update an existing shipper partner
        Task DeleteShipperPartnerAsync(int id);
        Task<int> GetTotalShipperPartnersAsync(); // Thêm phương thức đếm tổng
        Task<List<ShipperPartnerModel>> GetShipperPartnersByNameAsync(string name); // Thêm phương thức tìm theo tên
        Task<List<ShipperPartnerModel>> GetShipperPartnersByCompanyAsync(string company); /// Delete a shipper partner by ID
    }
}

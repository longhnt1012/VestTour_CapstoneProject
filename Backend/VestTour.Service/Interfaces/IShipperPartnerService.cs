using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IShipperPartnerService
    {
        Task<List<ShipperPartnerModel>> GetAllShipperPartnersAsync();
        Task<ShipperPartnerModel?> GetShipperPartnerByIdAsync(int id);
        Task<int> CreateShipperPartnerAsync(ShipperPartnerModel shipperPartnerModel);
        Task UpdateShipperPartnerAsync(int id, ShipperPartnerModel shipperPartnerModel);
        Task DeleteShipperPartnerAsync(int id);
        Task<int> GetTotalShipperPartnersAsync(); // Thêm phương thức đếm tổng
       
        Task<List<ShipperPartnerModel>> GetShipperPartnersByCompanyAsync(string company);
    }
}

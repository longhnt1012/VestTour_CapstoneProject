using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Implementation
{
    public class ShipperPartnerService : IShipperPartnerService
    {
        private readonly IShipperPartnerRepository _shipperPartnerRepository;

        public ShipperPartnerService(IShipperPartnerRepository shipperPartnerRepository)
        {
            _shipperPartnerRepository = shipperPartnerRepository;
        }

        public async Task<List<ShipperPartnerModel>> GetAllShipperPartnersAsync()
        {
            return await _shipperPartnerRepository.GetAllShipperPartnersAsync();
        }

        public async Task<ShipperPartnerModel?> GetShipperPartnerByIdAsync(int id)
        {
            return await _shipperPartnerRepository.GetShipperPartnerByIdAsync(id);
        }

        public async Task<int> CreateShipperPartnerAsync(ShipperPartnerModel shipperPartnerModel)
        {
            return await _shipperPartnerRepository.AddShipperPartnerAsync(shipperPartnerModel);
        }

        public async Task UpdateShipperPartnerAsync(int id, ShipperPartnerModel shipperPartnerModel)
        {
            await _shipperPartnerRepository.UpdateShipperPartnerAsync(id, shipperPartnerModel);
        }

        public async Task DeleteShipperPartnerAsync(int id)
        {
            await _shipperPartnerRepository.DeleteShipperPartnerAsync(id);
        }
        public async Task<int> GetTotalShipperPartnersAsync()
        {
            return await _shipperPartnerRepository.GetTotalShipperPartnersAsync(); // Gọi repository để lấy tổng
        }


        public async Task<List<ShipperPartnerModel>> GetShipperPartnersByCompanyAsync(string company)
        {
            return await _shipperPartnerRepository.GetShipperPartnersByCompanyAsync(company); // Gọi repository để tìm theo công ty
        }
    }
}

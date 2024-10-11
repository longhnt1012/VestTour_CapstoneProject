using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public class ShipperPartnerRepository : IShipperPartnerRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public ShipperPartnerRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all shipper partners
        public async Task<List<ShipperPartnerModel>> GetAllShipperPartnersAsync()
        {
            var shipperPartners = await _context.ShipperPartners!.ToListAsync();
            return _mapper.Map<List<ShipperPartnerModel>>(shipperPartners);
        }

        // Get shipper partner by ID
        public async Task<ShipperPartnerModel?> GetShipperPartnerByIdAsync(int id)
        {
            var shipperPartner = await _context.ShipperPartners!.FindAsync(id);
            return _mapper.Map<ShipperPartnerModel>(shipperPartner);
        }

        // Add a new shipper partner
        public async Task<int> AddShipperPartnerAsync(ShipperPartnerModel shipperPartnerModel)
        {
            var newShipperPartner = _mapper.Map<ShipperPartner>(shipperPartnerModel);
            _context.ShipperPartners!.Add(newShipperPartner);
            await _context.SaveChangesAsync();
            return newShipperPartner.ShipperPartnerId;
        }

        // Update an existing shipper partner
        public async Task UpdateShipperPartnerAsync(int id, ShipperPartnerModel shipperPartnerModel)
        {
            if (id == shipperPartnerModel.ShipperPartnerId)
            {
                var updatedShipperPartner = _mapper.Map<ShipperPartner>(shipperPartnerModel);
                _context.ShipperPartners!.Update(updatedShipperPartner);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a shipper partner by ID
        public async Task DeleteShipperPartnerAsync(int id)
        {
            var shipperPartner = await _context.ShipperPartners!.FindAsync(id);
            if (shipperPartner != null)
            {
                _context.ShipperPartners!.Remove(shipperPartner);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
using VestTour.Repository.Interfaces;

namespace VestTour.Repository.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public ShipmentRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShipmentModel>> GetAllShipmentsAsync()
        {
            var shipments = await _context.Shipments.ToListAsync();
            return _mapper.Map<List<ShipmentModel>>(shipments);
        }

        public async Task<ShipmentModel?> GetShipmentByIdAsync(int shipmentId)
        {
            var shipment = await _context.Shipments.FindAsync(shipmentId);
            return _mapper.Map<ShipmentModel>(shipment);
        }

        public async Task<int> AddShipmentAsync(ShipmentModel shipmentModel)
        {
            var shipment = _mapper.Map<Shipment>(shipmentModel);
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            return shipment.ShipmentId;
        }

        public async Task UpdateShipmentAsync(int shipmentId, ShipmentModel shipmentModel)
        {
            var shipment = _mapper.Map<Shipment>(shipmentModel);
            shipment.ShipmentId = shipmentId; // Ensure the correct ID is set
            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShipmentAsync(int shipmentId)
        {
            var shipment = await _context.Shipments.FindAsync(shipmentId);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShipmentModel>> GetShipmentsByStatusAsync(string status)
        {
            var shipments = await _context.Shipments
                .Where(s => s.Status == status)
                .ToListAsync();
            return _mapper.Map<List<ShipmentModel>>(shipments);
        }
        public async Task UpdateShipmentStatusAsync(int id, string status)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment != null)
            {
                shipment.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShipmentModel>> GetShipmentsByRecipientNameAsync(string recipientName)
        {
            var shipments = await _context.Shipments
                .Where(s => s.RecipientName != null && s.RecipientName.Contains(recipientName))
                .ToListAsync();

            return _mapper.Map<List<ShipmentModel>>(shipments);
        }
    }
}

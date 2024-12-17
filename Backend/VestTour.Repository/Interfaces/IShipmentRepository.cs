using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface IShipmentRepository
    {
        Task<List<ShipmentModel>> GetAllShipmentsAsync();
        Task<ShipmentModel?> GetShipmentByIdAsync(int shipmentId);
        Task<int> AddShipmentAsync(ShipmentModel shipmentModel);
        Task UpdateShipmentAsync(int shipmentId, ShipmentModel shipmentModel);
        Task DeleteShipmentAsync(int shipmentId);
        Task<List<ShipmentModel>> GetShipmentsByStatusAsync(string status);
        Task UpdateShipmentStatusAsync(int id, string status);
        Task<List<ShipmentModel>> GetShipmentsByRecipientNameAsync(string recipientName);
    }
}

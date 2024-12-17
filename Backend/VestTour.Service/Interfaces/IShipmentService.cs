using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IShipmentService
    {
        Task<ServiceResponse<ShipmentModel?>> GetShipmentByIdAsync(int shipmentId);
        Task<ServiceResponse<List<ShipmentModel>>> GetAllShipmentsAsync();
        Task<ServiceResponse<int>> AddShipmentAsync(ShipmentModel shipmentModel);
        Task<ServiceResponse> UpdateShipmentAsync(int shipmentId, ShipmentModel shipmentModel);
        Task<ServiceResponse> DeleteShipmentAsync(int shipmentId);
        Task<ServiceResponse<List<ShipmentModel>>> GetShipmentsByStatusAsync(string status);
        Task<ServiceResponse<List<ShipmentModel>>> GetShipmentsByRecipientNameAsync(string recipientName);
        Task<ServiceResponse> UpdateShipmentStatusAsync(int id, string status);
    }
}

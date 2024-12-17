using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;
using VestTour.Repository.Interfaces;
using VestTour.Service.Interfaces;
using VestTour.Domain.Entities;
using VestTour.Repository.ValidationHelper;

namespace VestTour.Service.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentService(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<ServiceResponse<ShipmentModel?>> GetShipmentByIdAsync(int shipmentId)
        {
            var response = new ServiceResponse<ShipmentModel?>();

            if (shipmentId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidShipmentId;
                return response;
            }

            try
            {
                var shipment = await _shipmentRepository.GetShipmentByIdAsync(shipmentId);
                if (shipment == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ShipmentNotFound}: {shipmentId}";
                }
                else
                {
                    response.Data = shipment;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ShipmentModel>>> GetAllShipmentsAsync()
        {
            var response = new ServiceResponse<List<ShipmentModel>>();

            try
            {
                var shipments = await _shipmentRepository.GetAllShipmentsAsync();
                if (!shipments.Any())
                {
                    response.Success = false;
                    response.Message = Error.NoShipmentsFound;
                }
                else
                {
                    response.Data = shipments;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> AddShipmentAsync(ShipmentModel shipmentModel)
        {
            var response = new ServiceResponse<int>();

            if (string.IsNullOrEmpty(shipmentModel.RecipientName) || string.IsNullOrEmpty(shipmentModel.Status))
            {
                response.Success = false;
                response.Message = Error.InvalidShipmentData;
                return response;
            }
            if (!ShipmentStatusValidate.IsValidStatus(shipmentModel.Status))
            {
                response.Success = false;
                response.Message = "Service type is not valid. Allowed types are:Pending, Shipped, Delivered,Finished.";
                return response;
            }
            try
            {
                var shipmentId = await _shipmentRepository.AddShipmentAsync(shipmentModel);
                response.Data = shipmentId;
                response.Message = Success.ShipmentAdded;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateShipmentAsync(int shipmentId, ShipmentModel shipmentModel)
        {
            var response = new ServiceResponse();

            if (shipmentId <= 0 || string.IsNullOrEmpty(shipmentModel.Status))
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }
            if (!ShipmentStatusValidate.IsValidStatus(shipmentModel.Status))
            {
                response.Success = false;
                response.Message = "Service type is not valid. Allowed types are:Pending, Shipped, Delivered,Finished.";
                return response;
            }
            try
            {
                await _shipmentRepository.UpdateShipmentAsync(shipmentId, shipmentModel);
                response.Message = Success.ShipmentUpdated;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteShipmentAsync(int shipmentId)
        {
            var response = new ServiceResponse();

            if (shipmentId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidShipmentId;
                return response;
            }

            try
            {
                await _shipmentRepository.DeleteShipmentAsync(shipmentId);
                response.Message = Success.ShipmentDeleted;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ShipmentModel>>> GetShipmentsByStatusAsync(string status)
        {
            var response = new ServiceResponse<List<ShipmentModel>>();

            if (string.IsNullOrEmpty(status))
            {
                response.Success = false;
                response.Message = Error.InvalidShipmentStatus;
                return response;
            }

            try
            {
                var shipments = await _shipmentRepository.GetShipmentsByStatusAsync(status);
                if (!shipments.Any())
                {
                    response.Success = false;
                    response.Message = Error.NoShipmentsFound;
                }
                else
                {
                    response.Data = shipments;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> UpdateShipmentStatusAsync(int id, string status)
        {
            var response = new ServiceResponse();

            if (id <= 0 || string.IsNullOrEmpty(status))
            {
                response.Success = false;
                response.Message = Error.InvalidShipmentStatus;
                return response;
            }

            try
            {
                await _shipmentRepository.UpdateShipmentStatusAsync(id, status);
                response.Success = true;
                response.Message = Success.ShipmentStatusUpdated;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ShipmentModel>>> GetShipmentsByRecipientNameAsync(string recipientName)
        {
            var response = new ServiceResponse<List<ShipmentModel>>();

            if (string.IsNullOrEmpty(recipientName))
            {
                response.Success = false;
                response.Message = Error.InvalidRecipientName;
                return response;
            }

            try
            {
                var shipments = await _shipmentRepository.GetShipmentsByRecipientNameAsync(recipientName);
                if (!shipments.Any())
                {
                    response.Success = false;
                    response.Message = Error.ShipmentNotFound;
                }
                else
                {
                    response.Data = shipments;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}

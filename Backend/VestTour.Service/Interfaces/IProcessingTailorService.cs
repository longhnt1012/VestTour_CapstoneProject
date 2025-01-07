using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IProcessingTailorService
    {
        Task<ServiceResponse<ProcessingTailorModel?>> GetProcessingTailorByIdAsync(int processingId);
        Task<ServiceResponse<List<ProcessingTailorModel>>> GetAllProcessingTailorsAsync();
        Task<ServiceResponse<int>> AddProcessingTailorAsync(ProcessingTailorModel processingTailor);
        Task<ServiceResponse> UpdateProcessingTailorAsync(int id, ProcessingTailorModel processingTailor);
        Task<ServiceResponse> DeleteProcessingTailorAsync(int processingId);
        Task<ServiceResponse<List<ProcessingTailorModel>>> GetProcessAssignedByTailorPartnerIdAsync(int tailorPartnerId);
        Task<ServiceResponse> ChangeStatusAsync(int processingId, string newStatus);
        Task<ServiceResponse> ChangeSampleStatusAsync(int processingId, string newStatus);
        Task<ServiceResponse> ChangeFixStatusAsync(int processingId, string newStatus);
        Task<ServiceResponse> ChangeDeliveryStatusAsync(int processingId, string newStatus);
        Task<ServiceResponse> ChangeStageNameAsync(int processingId, string newStage);
        Task<ServiceResponse<ProcessingTailorModel>> GetProcessingTailorsByOrderIdAsync(int orderId);
        Task<ServiceResponse<List<ProcessingTailorModel>>> GetProcessingTailorsByStoreIdAsync(int storeId);
    }
}

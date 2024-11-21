using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface IProcessingTailorRepository
    {
        Task<List<ProcessingTailorModel>> GetAllProcessingTailorsAsync();
        Task<ProcessingTailorModel?> GetProcessingTailorByIdAsync(int processingId);
        Task<int> AddProcessingTailorAsync(ProcessingTailorModel processingTailor);
        Task UpdateProcessingTailorAsync(int id, ProcessingTailorModel processingTailor);
        Task DeleteProcessingTailorAsync(int processingId);
        Task<List<ProcessingTailorModel>> GetProcessAssignedByTailorPartnerIdAsync(int tailorPartnerId);
        Task ChangeStatusAsync(int processingId, string newStatus);
        Task ChangeSampleStatusAsync(int processingId, string newStatus);
        Task ChangeFixStatusAsync(int processingId, string newStatus);
        Task ChangeDeliveryStatusAsync(int processingId, string newStatus);
    }
}

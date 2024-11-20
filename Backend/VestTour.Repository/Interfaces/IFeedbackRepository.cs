using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<List<FeedbackModel>> GetFeedbacksByOrderIdAsync(int orderId);
        Task<List<FeedbackModel>> GetFeedbacksByProductIdAsync(int productId);
        Task<FeedbackModel?> GetFeedbackByIdAsync(int feedbackId);
        Task<int> AddFeedbackAsync(WriteFeedbackModel feedback);
        Task UpdateFeedbackAsync(int feedbackId, WriteFeedbackModel feedback);
        Task ResponseFeedbackAsync(int feedbackId, ResponseFeedbackModel feedback);
        Task DeleteFeedbackAsync(int feedbackId);
        Task<List<FeedbackModel>> GetAllFeedbackAsync();
        Task<List<FeedbackModel>> GetFeedbacksByUserIdAsync(int userId);
        
    }
}

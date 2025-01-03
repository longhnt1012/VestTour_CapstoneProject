﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<ServiceResponse<List<FeedbackModel>>> GetFeedbacksByOrderIdAsync(int orderId);
        Task<ServiceResponse<List<FeedbackModel>>> GetFeedbacksByProductIdAsync(int productId);
        Task<ServiceResponse<int>> AddFeedbackForProductAsync(FeedBackForProduct feedback);
        Task<ServiceResponse<int>> AddFeedbackForOrderAsync(FeedbackForOrder feedback);
        Task<ServiceResponse> UpdateFeedbackForProductAsync(int feedbackId, FeedBackForProduct feedback);
        Task<ServiceResponse> UpdateFeedbackForOrderAsync(int feedbackId, FeedbackForOrder feedback);
        Task<ServiceResponse> ResponseFeedbackAsync(int feedbackId, ResponseFeedbackModel feedback);
        Task<ServiceResponse> DeleteFeedbackAsync(int feedbackId);

        Task<ServiceResponse<List<FeedbackModel>>> GetAllFeedbackAsync();
        Task<ServiceResponse<List<FeedbackModel>>> GetFeedbacksByUserIdAsync(int userId);
    }
}

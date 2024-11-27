using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<ServiceResponse<List<FeedbackModel>>> GetFeedbacksByOrderIdAsync(int orderId)
        {
            var response = new ServiceResponse<List<FeedbackModel>>();

            try
            {
                response.Data = await _feedbackRepository.GetFeedbacksByOrderIdAsync(orderId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<FeedbackModel>>> GetFeedbacksByProductIdAsync(int productId)
        {
            var response = new ServiceResponse<List<FeedbackModel>>();

            try
            {
                response.Data = await _feedbackRepository.GetFeedbacksByProductIdAsync(productId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> AddFeedbackForProductAsync(FeedBackForProduct feedback)
        {
            var response = new ServiceResponse<int>();

            try
            {
                response.Data = await _feedbackRepository.AddFeedbackForProductAsync(feedback);
                response.Success = true;
                response.Message = "Feedback added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse<int>> AddFeedbackForOrderAsync(FeedbackForOrder feedback)
        {
            var response = new ServiceResponse<int>();

            try
            {
                response.Data = await _feedbackRepository.AddFeedbackForOrderAsync(feedback);
                response.Success = true;
                response.Message = "Feedback added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> UpdateFeedbackForProductAsync(int feedbackId, FeedBackForProduct feedback)
        {
            var response = new ServiceResponse();

            try
            {
                await _feedbackRepository.UpdateFeedbackForProductAsync(feedbackId, feedback);
                response.Success = true;
                response.Message = "Feedback updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> UpdateFeedbackForOrderAsync(int feedbackId, FeedbackForOrder feedback)
        {
            var response = new ServiceResponse();

            try
            {
                await _feedbackRepository.UpdateFeedbackForOrderAsync(feedbackId, feedback);
                response.Success = true;
                response.Message = "Feedback updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> ResponseFeedbackAsync(int feedbackId, ResponseFeedbackModel feedback)
        {
            var response = new ServiceResponse();

            try
            {
                await _feedbackRepository.ResponseFeedbackAsync(feedbackId, feedback);
                response.Success = true;
                response.Message = "Feedback updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse<List<FeedbackModel>>> GetAllFeedbackAsync()
        {
            var response = new ServiceResponse<List<FeedbackModel>>();

            try
            {
                response.Data = await _feedbackRepository.GetAllFeedbackAsync();
                response.Success = true;
                response.Message = response.Data.Any() ? null : "No feedbacks found.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<FeedbackModel>>> GetFeedbacksByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<List<FeedbackModel>>();

            try
            {
                response.Data = await _feedbackRepository.GetFeedbacksByUserIdAsync(userId);
                response.Success = true;
                response.Message = response.Data.Any() ? null : "No feedbacks found for this user.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse> DeleteFeedbackAsync(int feedbackId)
        {
            var response = new ServiceResponse();

            try
            {
                await _feedbackRepository.DeleteFeedbackAsync(feedbackId);
                response.Success = true;
                response.Message = "Feedback deleted successfully.";
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

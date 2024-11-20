using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;

namespace VestTour.Repository.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public FeedbackRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FeedbackModel>> GetFeedbacksByOrderIdAsync(int orderId)
        {
            var feedbacks = await _context.Feedbacks
                                          .Where(f => f.OrderId == orderId)
                                          .ToListAsync();
            return _mapper.Map<List<FeedbackModel>>(feedbacks);
        }

        public async Task<List<FeedbackModel>> GetFeedbacksByProductIdAsync(int productId)
        {
            var feedbacks = await _context.Feedbacks
                                          .Where(f => f.ProductId == productId)
                                          .ToListAsync();
            return _mapper.Map<List<FeedbackModel>>(feedbacks);
        }

        public async Task<FeedbackModel?> GetFeedbackByIdAsync(int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            return _mapper.Map<FeedbackModel>(feedback);
        }

        public async Task<int> AddFeedbackAsync(WriteFeedbackModel feedback)
        {
            if (feedback.Rating.HasValue && (feedback.Rating < 1 || feedback.Rating > 5))
            {
                throw new ArgumentOutOfRangeException(nameof(feedback.Rating), "Rating must be between 1 and 5.");
            }

            var newFeedback = _mapper.Map<Feedback>(feedback);
            _context.Feedbacks.Add(newFeedback);
            await _context.SaveChangesAsync();
            return newFeedback.FeedbackId;
        }


        public async Task UpdateFeedbackAsync(int feedbackId, WriteFeedbackModel feedback)
        {

            var existingFeedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (existingFeedback != null)
            {
                _mapper.Map(feedback, existingFeedback);
                _context.Feedbacks.Update(existingFeedback);
                await _context.SaveChangesAsync();
            }
        }
        public async Task ResponseFeedbackAsync(int feedbackId, ResponseFeedbackModel feedback)
        {
            var existingFeedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (existingFeedback != null)
            {
                _mapper.Map(feedback, existingFeedback);
                _context.Feedbacks.Update(existingFeedback);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<FeedbackModel>> GetAllFeedbackAsync()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();
            return _mapper.Map<List<FeedbackModel>>(feedbacks);
        }

        public async Task<List<FeedbackModel>> GetFeedbacksByUserIdAsync(int userId)
        {
            var feedbacks = await _context.Feedbacks
                                          .Where(f => f.UserId == userId)
                                          .ToListAsync();
            return _mapper.Map<List<FeedbackModel>>(feedbacks);
        }
    }
    }

﻿using AutoMapper;
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

namespace VestTour.Repository.Implementation
{
    public class ProcessingTailorRepository : IProcessingTailorRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public ProcessingTailorRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProcessingTailorModel>> GetAllProcessingTailorsAsync()
        {
            var processingTailors = await _context.ProcessingTailors.ToListAsync();
            return _mapper.Map<List<ProcessingTailorModel>>(processingTailors);
        }

        public async Task<ProcessingTailorModel?> GetProcessingTailorByIdAsync(int processingId)
        {
            var processingTailor = await _context.ProcessingTailors.FindAsync(processingId);
            return _mapper.Map<ProcessingTailorModel>(processingTailor);
        }

        public async Task<int> AddProcessingTailorAsync(ProcessingTailorModel processingTailor)
        {
            var newProcessingTailor = _mapper.Map<ProcessingTailor>(processingTailor);
            _context.ProcessingTailors.Add(newProcessingTailor);
            await _context.SaveChangesAsync();
            return newProcessingTailor.ProcessingId;
        }

        public async Task UpdateProcessingTailorAsync(int id, ProcessingTailorModel processingTailor)
        {
            var updatedProcessingTailor = _mapper.Map<ProcessingTailor>(processingTailor);
            _context.ProcessingTailors.Update(updatedProcessingTailor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProcessingTailorAsync(int processingId)
        {
            var processingTailor = await _context.ProcessingTailors.FindAsync(processingId);
            if (processingTailor != null)
            {
                _context.ProcessingTailors.Remove(processingTailor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
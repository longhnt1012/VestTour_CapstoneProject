using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VestTour.Models;

namespace VestTour.Services
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public MeasurementRepository(VestTourDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<MeasurementModel> AddMeasurementAsync(MeasurementModel measurement)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMeasurementAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<MeasurementModel> GetMeasurementByIdAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MeasurementModel>> GetMeasurementsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMeasurementAsync(MeasurementModel measurement)
        {
            throw new NotImplementedException();
        }
    }
}

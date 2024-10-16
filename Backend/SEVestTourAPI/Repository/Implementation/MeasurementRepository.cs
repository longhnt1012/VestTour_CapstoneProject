using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Repository.Implementation
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

        // Get measurement by ID
        public async Task<MeasurementModel?> GetMeasurementByIdAsync(int measurementId)
        {
            var measurement = await _context.Measurements!.FindAsync(measurementId);
            return _mapper.Map<MeasurementModel>(measurement);
        }

        // Get all measurements
        public async Task<List<MeasurementModel>> GetAllMeasurementsAsync()
        {
            var measurements = await _context.Measurements!.ToListAsync();
            return _mapper.Map<List<MeasurementModel>>(measurements);
        }

        // Add a new measurement
        public async Task<int> AddMeasurementAsync(MeasurementModel measurementModel)
        {
            var newMeasurement = _mapper.Map<Measurement>(measurementModel);
            _context.Measurements!.Add(newMeasurement);
            await _context.SaveChangesAsync();
            return newMeasurement.MeasurementId;
        }

        // Update measurement details
        public async Task UpdateMeasurementAsync(int id, MeasurementModel measurementModel)
        {
            if (id == measurementModel.MeasurementId)
            {
                var updateMeasurement = _mapper.Map<Measurement>(measurementModel);
                _context.Measurements!.Update(updateMeasurement);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a measurement by ID
        public async Task DeleteMeasurementAsync(int measurementId)
        {
            var deleteMeasurement = await _context.Measurements!.SingleOrDefaultAsync(m => m.MeasurementId == measurementId);
            if (deleteMeasurement != null)
            {
                _context.Measurements!.Remove(deleteMeasurement);
                await _context.SaveChangesAsync();
            }
        }
    }
}

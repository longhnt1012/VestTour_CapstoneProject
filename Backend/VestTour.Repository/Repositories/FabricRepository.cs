using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Domain.Enums; // Add this using directive
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VestTour.Repository.Implementation
{
    public class FabricRepository : IFabricRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public FabricRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddFabricAsync(FabricModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Fabric model cannot be null.");
            }

            var newFabric = _mapper.Map<Fabric>(model);
            _context.Fabrics!.Add(newFabric);
            await _context.SaveChangesAsync();

            return newFabric.FabricId;
        }

        public async Task DeleteFabricAsync(int fabricId)
        {
            var deleteFabric = await _context.Fabrics!.SingleOrDefaultAsync(b => b.FabricId == fabricId);
            if (deleteFabric != null)
            {
                _context.Fabrics!.Remove(deleteFabric);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Fabric with ID {fabricId} not found.");
            }
        }

        public async Task<List<FabricModel>> GetAllFabricsAsync()
        {
            var fabrics = await _context.Fabrics!.ToListAsync();
            return _mapper.Map<List<FabricModel>>(fabrics);
        }

        public async Task<FabricModel> GetFabricModelByIdAsync(int fabricId)
        {
            var fabric = await _context.Fabrics!.FindAsync(fabricId);
            if (fabric == null)
            {
                throw new KeyNotFoundException($"Fabric with ID {fabricId} not found.");
            }
            return _mapper.Map<FabricModel>(fabric);
        }

        public async Task UpdateFabricAsync(int id, FabricModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Fabric model cannot be null.");
            }

            if (id != model.FabricId)
            {
                throw new ArgumentException("Fabric ID mismatch");
            }

            var updateFabric = _mapper.Map<Fabric>(model);
            _context.Fabrics!.Update(updateFabric);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FabricModel>> GetFabricByTagAsync(FabricEnums? tag) // Use nullable enum
        {
            var fabricsQuery = _context.Fabrics.AsQueryable();

            if (tag.HasValue) // Check if tag is not null
            {
                fabricsQuery = fabricsQuery.Where(f => f.Tag == tag.Value); // Use Value to access the enum
            }

            var fabrics = await fabricsQuery.ToListAsync();
            return _mapper.Map<List<FabricModel>>(fabrics);
        }
        public async Task<List<FabricModel>> GetFabricsByDescriptionAsync(string description)
        {
            var fabrics = await _context.Fabrics
                                        .Where(f => f.Description.Contains(description)) // Sử dụng Contains để tìm kiếm từ khóa
                                        .ToListAsync();

            return _mapper.Map<List<FabricModel>>(fabrics);
        }

    }

}

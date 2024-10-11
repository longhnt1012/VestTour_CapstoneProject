using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Models;

namespace VestTour.Services
{
    public class FabricRepository : IFabricRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public FabricRepository(VestTourDbContext context,IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddFabricAsync(FabricModel fabric)
        {
            var newFabric= _mapper.Map<Fabric>(fabric);
            _context.Fabrics!.Add(newFabric);
            await _context.SaveChangesAsync();

            return newFabric.FabricId;
        }

        public async Task DeleteFabricAsync(int fabricId)
        {
           var deleteFabric= _context.Fabrics!.SingleOrDefault(b => b.FabricId==fabricId);
            if (deleteFabric != null)
            {
                _context.Fabrics!.Remove(deleteFabric);
                await _context.SaveChangesAsync() ;
            }
        }

        public async Task<List<FabricModel>> GetAllFabricsAsync()
        {
            var fabrics = await _context.Fabrics!.ToListAsync();
             return  _mapper.Map<List<FabricModel>>(fabrics);
        }

        public async Task<FabricModel?> GetFabricByIdAsync(int fabricId)
        {   
            var fabric = await _context.Fabrics!.FindAsync(fabricId);
            return _mapper.Map<FabricModel>(fabric);
        }

        public async Task UpdateFabricAsync(int id, FabricModel fabric)
        {
            if(id == fabric.FabricId)
            {
                var updateFabric = _mapper.Map<Fabric>(fabric);
                _context.Fabrics!.Update(updateFabric);
                await _context.SaveChangesAsync();
            }
        }
    }  
}

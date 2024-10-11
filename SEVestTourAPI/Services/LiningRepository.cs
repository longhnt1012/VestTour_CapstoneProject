using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public class LiningRepository : ILiningRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public LiningRepository(VestTourDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper= mapper;
        }

        public async Task<int> AddLiningAsync(LiningModel lining)
        {
            var newLining = _mapper.Map<Lining>(lining);
            _context.Linings.Add(newLining);
            await _context.SaveChangesAsync();

            return newLining.LiningId;
        }

        public async Task DeleteLiningAsync(int id)
        {
            var deleteLining = _context.Linings!.SingleOrDefault(l => l.LiningId == id);
            if (deleteLining != null)
            {
                _context.Linings!.Remove(deleteLining);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LiningModel>> GetAllLiningAsync()
        {
            var linings = await _context.Linings!.ToListAsync();
            return _mapper.Map<List<LiningModel>>(linings);
        }

        public async Task<LiningModel> GetLiningByIdAsync(int id)
        {
            var lining = await _context.Linings!.FirstOrDefaultAsync(li => li.LiningId == id);
            return _mapper.Map<LiningModel>(lining);
        }

        public async Task UpdateLiningAsync(int id, LiningModel lining)
        {
            if (id == lining.LiningId)
            {
                var updateLining = _mapper.Map<Lining>(lining);
                _context.Linings!.Update(updateLining);
                await _context.SaveChangesAsync();
            }
        }
    }
    
}

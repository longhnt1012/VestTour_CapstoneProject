using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Repository.Models;
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;
namespace VestTour.Repository.Repositories
{
    public class StyleOptionRepository : IStyleOptionRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public StyleOptionRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<decimal?> GetStyleOptionPriceByIdAsync(int styleOptionId)
        {
            var styleOption = await _context.StyleOptions.FirstOrDefaultAsync(so => so.StyleOptionId == styleOptionId);
            return styleOption?.Price;
        }
        // Get all style options
        public async Task<List<StyleOptionModel>> GetAllStyleOptionsAsync()
        {
            var styleOptions = await _context.StyleOptions!.ToListAsync();
            return _mapper.Map<List<StyleOptionModel>>(styleOptions);
        }

        // Get style option by ID
        public async Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id)
        {
            var styleOption = await _context.StyleOptions!.FindAsync(id);
            return _mapper.Map<StyleOptionModel>(styleOption);
        }

        // Add a new style option
        public async Task<int> AddStyleOptionAsync(StyleOptionModel styleOptionModel)
        {
            var newStyleOption = _mapper.Map<StyleOption>(styleOptionModel);
            _context.StyleOptions!.Add(newStyleOption);
            await _context.SaveChangesAsync();
            return newStyleOption.StyleOptionId;
        }

        // Update an existing style option
        public async Task UpdateStyleOptionAsync(int id, StyleOptionModel styleOptionModel)
        {
            if (id == styleOptionModel.StyleOptionId)
            {
                var updatedStyleOption = _mapper.Map<StyleOption>(styleOptionModel);
                _context.StyleOptions!.Update(updatedStyleOption);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a style option by ID
        public async Task DeleteStyleOptionAsync(int id)
        {
            var styleOption = await _context.StyleOptions!.FindAsync(id);
            if (styleOption != null)
            {
                _context.StyleOptions!.Remove(styleOption);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateStatusAsync(int itemId, string newStatus)
        {
            var item = await _context.StyleOptions.FindAsync(itemId);
            if (item != null)
            {
                item.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}

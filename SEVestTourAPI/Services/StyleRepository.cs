using AutoMapper;
using Microsoft.EntityFrameworkCore;
<<<<<<<< Updated upstream:SEVestTourAPI/Services/StyleRepository.cs
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
========
using VestTour.Domain.Entities;
using VestTour.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Repository.Data;
namespace VestTour.Repository.Implementation
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/StyleRepository.cs
{
    public class StyleRepository : IStyleRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public StyleRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all styles
        public async Task<List<StyleModel>> GetAllStylesAsync()
        {
            var styles = await _context.Styles.ToListAsync();
            return _mapper.Map<List<StyleModel>>(styles);
        }

        // Get style by ID
        public async Task<StyleModel?> GetStyleByIdAsync(int id)
        {
            var style = await _context.Styles!.FindAsync(id);
            return _mapper.Map<StyleModel>(style);
        }

        // Add a new style
        public async Task<int> AddStyleAsync(StyleModel styleModel)
        {
            var newStyle = _mapper.Map<Style>(styleModel);
            _context.Styles!.Add(newStyle);
            await _context.SaveChangesAsync();
            return newStyle.StyleId;
        }

        // Update an existing style
        public async Task UpdateStyleAsync(int id, StyleModel styleModel)
        {
            if (id == styleModel.StyleId)
            {
                var updatedStyle = _mapper.Map<Style>(styleModel);
                _context.Styles!.Update(updatedStyle);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a style by ID
        public async Task DeleteStyleAsync(int id)
        {
            var style = await _context.Styles!.FindAsync(id);
            if (style != null)
            {
                _context.Styles!.Remove(style);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Implementation
{
    public class StyleService : IStyleService
    {
        private readonly IStyleRepository _styleRepository;

        public StyleService(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        public async Task<List<StyleModel>> GetAllStylesAsync()
        {
            return await _styleRepository.GetAllStylesAsync();
        }

        public async Task<StyleModel?> GetStyleByIdAsync(int id)
        {
            return await _styleRepository.GetStyleByIdAsync(id);
        }

        public async Task<int> AddStyleAsync(StyleModel styleModel)
        {
            return await _styleRepository.AddStyleAsync(styleModel);
        }

        public async Task UpdateStyleAsync(int id, StyleModel styleModel)
        {
            await _styleRepository.UpdateStyleAsync(id, styleModel);
        }

        public async Task DeleteStyleAsync(int id)
        {
            await _styleRepository.DeleteStyleAsync(id);
        }

    }
}

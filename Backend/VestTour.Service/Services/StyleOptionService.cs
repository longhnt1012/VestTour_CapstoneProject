using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Implementation
{
    public class StyleOptionService : IStyleOptionService
    {
        private readonly IStyleOptionRepository _styleOptionRepository;

        public StyleOptionService(IStyleOptionRepository styleOptionRepository)
        {
            _styleOptionRepository = styleOptionRepository;
        }

        public async Task<List<StyleOptionModel>> GetAllStyleOptionsAsync()
        {
            return await _styleOptionRepository.GetAllStyleOptionsAsync();
        }

        public async Task<StyleOptionModel?> GetStyleOptionByIdAsync(int id)
        {
            return await _styleOptionRepository.GetStyleOptionByIdAsync(id);
        }

        public async Task<int> AddStyleOptionAsync(StyleOptionModel styleOptionModel)
        {
            return await _styleOptionRepository.AddStyleOptionAsync(styleOptionModel);
        }

        public async Task UpdateStyleOptionAsync(int id, StyleOptionModel styleOptionModel)
        {
            await _styleOptionRepository.UpdateStyleOptionAsync(id, styleOptionModel);
        }

        public async Task DeleteStyleOptionAsync(int id)
        {
            await _styleOptionRepository.DeleteStyleOptionAsync(id);
        }
    }
}

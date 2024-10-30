using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using VestTour.Domain.Entities;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.Repository.Services
{
    public class ProductStyleOptionervice : IProductStyleOptionervice
    {
        private readonly IProductStyleOptionRepository _productStyleOptionRepository;
        private readonly IMapper _mapper;

        public ProductStyleOptionervice(IProductStyleOptionRepository productStyleOptionRepository, IMapper mapper)
        {
            _productStyleOptionRepository = productStyleOptionRepository;
            _mapper = mapper;
        }

        public async Task AddStyleOptionAsync(int productId, int styleOptionId)
        {
            var productStyleOption = new ProductStyleOptionModel
            {
                ProductId = productId,
                StyleOptionId = styleOptionId
            };

            await _productStyleOptionRepository.AddProductStyleOptionAsync(productStyleOption);
        }

        public async Task<IEnumerable<ProductStyleOptionModel>> GetStyleOptionsByProductIdAsync(int productId)
        {
            var options = await _productStyleOptionRepository.GetByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ProductStyleOptionModel>>(options);
        }

        public async Task DeleteStyleOptionsByProductIdAsync(int productId)
        {
            await _productStyleOptionRepository.DeleteByProductIdAsync(productId);
        }
    }
}

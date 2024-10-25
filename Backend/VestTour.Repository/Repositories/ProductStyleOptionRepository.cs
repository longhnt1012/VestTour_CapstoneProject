using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using VestTour.Repository.Models;
using AutoMapper;

namespace VestTour.Repository.Repositories
{
    public class ProductStyleOptionRepository : IProductStyleOptionRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;
        public ProductStyleOptionRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductStyleOptionModel productStyleOption)
        {
            var productOption = _mapper.Map<ProductStyleOption>(productStyleOption);
            _context.ProductStyleOptions.Add(productOption);
            await _context.SaveChangesAsync();
            

        }

        public async Task<IEnumerable<ProductStyleOption>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductStyleOptions
                                .Where(p => p.ProductId == productId)
                                .ToListAsync();
        }
    }
}

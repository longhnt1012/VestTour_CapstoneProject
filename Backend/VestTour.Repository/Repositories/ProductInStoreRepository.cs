using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Data;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;

namespace VestTour.Repository.Repositories
{
    public class ProductInStoreRepository : IProductInStoreRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public ProductInStoreRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductInStoreModel?> GetProductInStoreAsync(int storeId, int productId)
        {
            var productInStore = await _context.ProductInStores
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.ProductId == productId);
            return _mapper.Map<ProductInStoreModel>(productInStore);
        }

        public async Task<List<ProductInStoreModel>> GetAllProductsInStoreAsync()
        {
            var productsInStore = await _context.ProductInStores.ToListAsync();
            return _mapper.Map<List<ProductInStoreModel>>(productsInStore);
        }

        public async Task<int> AddProductInStoreAsync(ProductInStoreModel productInStore)
        {
            var newProductInStore = _mapper.Map<ProductInStore>(productInStore);
            _context.ProductInStores.Add(newProductInStore);
            await _context.SaveChangesAsync();
            return newProductInStore.ProductId;
        }

        public async Task UpdateProductInStoreAsync(int storeId, int productId, ProductInStoreModel productInStore)
        {
            var existingProductInStore = await _context.ProductInStores
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.ProductId == productId);

            if (existingProductInStore != null)
            {
                _mapper.Map(productInStore, existingProductInStore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductInStoreAsync(int storeId, int productId)
        {
            var productInStore = await _context.ProductInStores
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.ProductId == productId);

            if (productInStore != null)
            {
                _context.ProductInStores.Remove(productInStore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(int storeId, int productId, int quantity)
        {
            var productInStore = await _context.ProductInStores
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.ProductId == productId);

            if (productInStore != null)
            {
                productInStore.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}

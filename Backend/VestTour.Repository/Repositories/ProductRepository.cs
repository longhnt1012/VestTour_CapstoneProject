﻿using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Data;
namespace VestTour.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<ProductModel?> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductModel?>(product);
        }
        public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _context.Products
                                         .Where(p => p.CategoryId == categoryId)
                                         .ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }
        public async Task<List<ProductModel>> GetProductsByCategoryIdIsCustomFalseAsync(int categoryId)
        {
            var products = await _context.Products
                                         .Where(p => p.CategoryId == categoryId && p.IsCustom == false)
                                         .ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<int> AddProductAsync(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task UpdateProductAsync(int id, ProductModel productModel)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct != null)
            {
                _mapper.Map(productModel, existingProduct);
                await _context.SaveChangesAsync();
            }
        }
        //public async Task AddProductStyleOptionAsync(ProductStyleOptionModel model)
        //{
        //    var entity = new ProductStyleOption
        //    {
        //        ProductId = model.ProductId,
        //        StyleOptionId = model.StyleOptionId
        //    };
        //    _context.ProductStyleOptions.Add(entity);
        //    await _context.SaveChangesAsync();
        //}
        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductModel?> GetProductByCodeAsync(string productCode)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductCode == productCode);
            return _mapper.Map<ProductModel?>(product);
        }
        public async Task<List<ProductModel>> GetProductsWithIsCustomFalseAsync()
        {
            var products = await _context.Products
                                         .Where(p => p.IsCustom == false)
                                         .ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }
        public async Task<List<ProductQuantityModel>> GetProductsWithIsCustomFalseInStoreAsync(int storeId)
        {
            var productsWithQuantities = await _context.Products
                .Where(p => p.IsCustom == false)
                .Join(
                    _context.ProductInStores, // Joining with ProductInStore table
                    product => product.ProductId, // Primary key in Products
                    productInStore => productInStore.ProductId, // Foreign key in ProductInStore
                    (product, productInStore) => new
                    {
                        Product = product,
                        ProductInStore = productInStore
                    }
                )
                .Where(joined => joined.ProductInStore.StoreId == storeId) 
                .Select(joined => new ProductQuantityModel
                {
                    ProductId = joined.Product.ProductId,
                    ProductCode = joined.Product.ProductCode,
                    Price = joined.Product.Price,
                    Quantity = joined.ProductInStore.Quantity 
                })
                .ToListAsync();

            return productsWithQuantities;
        }
        public async Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId)
        {
            // Load the product along with its related entities, including the many-to-many StyleOptions
            var product = await (from p in _context.Products
                                 join f in _context.Fabrics on p.FabricId equals f.FabricId into pf
                                 from f in pf.DefaultIfEmpty()
                                 join l in _context.Linings on p.LiningId equals l.LiningId into pl
                                 from l in pl.DefaultIfEmpty()
                                 where p.ProductId == productId
                                 select new ProductDetailsModel
                                 {
                                     ProductID = p.ProductId,
                                     ProductCode = p.ProductCode,
                                     MeasurementID = p.MeasurementId,
                                     CategoryID = p.CategoryId,
                                     FabricID = p.FabricId,
                                     LiningID = p.LiningId,
                                     FabricName = f != null ? f.FabricName : null,
                                     LiningName = l != null ? l.LiningName : null,
                                     Price = p.Price,
                                     Size = p.Size,
                                     ImgURL = p.ImgUrl,
                                     StyleOptions = p.StyleOptions.Select(so => new StyleOptionModel
                                     {
                                         StyleOptionId = so.StyleOptionId,
                                         StyleId = so.StyleId.Value,
                                         OptionType = so.OptionType,
                                         OptionValue = so.OptionValue
                                     }).ToList()
                                 }).FirstOrDefaultAsync();

            return product;
        }
        public async Task<string?> GetProductCodeByIdAsync(int productId)
        {
            var product = await _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => p.ProductCode)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task UpdateStatusAsync(int productId, string newStatus)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}

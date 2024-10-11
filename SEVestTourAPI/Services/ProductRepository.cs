using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
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

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId)
        {
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
                                     OrderID = p.OrderId,
                                     FabricName = f != null ? f.FabricName : null,
                                     LiningName = l != null ? l.LiningName : null,

                                     StyleOptions = (from pso in _context.ProductStyleOptions
                                                     join so in _context.StyleOptions on pso.StyleOptionId equals so.StyleOptionId
                                                     where pso.ProductId == p.ProductId
                                                     select new StyleOptionModel
                                                     {
                                                       //no style nem
                                                       StyleOptionId=so.StyleOptionId,
                                                         OptionType = so.OptionType,
                                                         OptionValue = so.OptionValue
                                                     }).ToList()
                                 }).FirstOrDefaultAsync();

            return product;
        }

    }
}

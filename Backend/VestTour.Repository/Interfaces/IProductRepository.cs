﻿using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductRepository
{
    Task<List<ProductModel>> GetAllProductsAsync();
    Task<ProductModel?> GetProductByIdAsync(int productId);
    Task<ProductModel?> GetProductByCodeAsync(string productCode);
    Task<int> AddProductAsync(ProductModel product);
    Task UpdateProductAsync(int id, ProductModel product);
    Task DeleteProductAsync(int productId);
    Task<ProductDetailsModel> GetProductWithDetailsAsync(int productId);
    Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId);
    Task<List<ProductModel>> GetProductsWithIsCustomFalseAsync();
}

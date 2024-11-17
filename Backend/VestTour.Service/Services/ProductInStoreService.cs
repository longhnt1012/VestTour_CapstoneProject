using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Constants;
using VestTour.Repository.Interfaces;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class ProductInStoreService : IProductInStoreService
    {
        private readonly IProductInStoreRepository _productInStoreRepository;

        public ProductInStoreService(IProductInStoreRepository productInStoreRepository)
        {
            _productInStoreRepository = productInStoreRepository;
        }

        public async Task<ServiceResponse<ProductInStoreModel?>> GetProductInStoreAsync(int storeId, int productId)
        {
            var response = new ServiceResponse<ProductInStoreModel?>();

            if (storeId <= 0 || productId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }

            try
            {
                var productInStore = await _productInStoreRepository.GetProductInStoreAsync(storeId, productId);
                if (productInStore == null)
                {
                    response.Success = false;
                    response.Message = $"{Error.ProductNotFound}: StoreId={storeId}, ProductId={productId}";
                }
                else
                {
                    response.Data = productInStore;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ProductInStoreModel>>> GetAllProductsInStoreAsync()
        {
            var response = new ServiceResponse<List<ProductInStoreModel>>();

            try
            {
                var productsInStore = await _productInStoreRepository.GetAllProductsInStoreAsync();

                if (!productsInStore.Any())
                {
                    response.Success = false;
                    response.Message = Error.ProductNotFound;
                }
                else
                {
                    response.Data = productsInStore;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<int>> AddProductInStoreAsync(ProductInStoreModel productInStore)
        {
            var response = new ServiceResponse<int>();

            if (productInStore.StoreID <= 0 || productInStore.ProductID <= 0 || productInStore.Quantity < 0)
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }

            try
            {
                var newProductId = await _productInStoreRepository.AddProductInStoreAsync(productInStore);
                response.Data = newProductId;
                response.Message = Success.ProductAddedToStore;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateProductInStoreAsync(int storeId, int productId, ProductInStoreModel productInStore)
        {
            var response = new ServiceResponse();

            if (storeId <= 0 || productId <= 0 || productInStore.Quantity < 0)
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }

            try
            {
                await _productInStoreRepository.UpdateProductInStoreAsync(storeId, productId, productInStore);
                response.Message = Success.ProductUpdatedInStore;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteProductInStoreAsync(int storeId, int productId)
        {
            var response = new ServiceResponse();

            if (storeId <= 0 || productId <= 0)
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }

            try
            {
                await _productInStoreRepository.DeleteProductInStoreAsync(storeId, productId);
                response.Message = Success.ProductDeletedFromStore;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateQuantityAsync(int storeId, int productId, int quantity)
        {
            var response = new ServiceResponse();

            if (storeId <= 0 || productId <= 0 || quantity < 0)
            {
                response.Success = false;
                response.Message = Error.InvalidInputData;
                return response;
            }

            try
            {
                await _productInStoreRepository.UpdateQuantityAsync(storeId, productId, quantity);
                response.Message = Success.QuantityUpdatedInStore;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

    }
}

using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestTour.Services
{
    public class AddCartService : IAddCartService
    {
        private readonly IAddCartRepository _addCartRepository;
        private readonly IProductService _productService;

        public AddCartService(IAddCartRepository addCartRepository, IProductService productService)
        {
            _addCartRepository = addCartRepository;
            _productService = productService;
        }

        public async Task AddToCartAsync(int userId, int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            var cartItem = new CartItemModel
            {
                ProductId = productId,
                Price = product.Price,
                Quantity = 1
            };

            await _addCartRepository.AddToCartAsync(userId, cartItem);
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            await _addCartRepository.RemoveFromCartAsync(userId, productId);
        }

        public async Task<List<CartItemModel>> GetUserCartAsync(int userId)
        {
            return await _addCartRepository.GetUserCartAsync(userId);
        }

        // Method to decrease quantity by 1
        public async Task DecreaseQuantityAsync(int userId, int productId)
        {
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            var item = cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }

            await _addCartRepository.UpdateCartAsync(userId, cartItems);
        }

        // Method to increase quantity by 1
        public async Task IncreaseQuantityAsync(int userId, int productId)
        {
            var cartItems = await _addCartRepository.GetUserCartAsync(userId);
            var item = cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
            }

            await _addCartRepository.UpdateCartAsync(userId, cartItems);
        }
    }
}

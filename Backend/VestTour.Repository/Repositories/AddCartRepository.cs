using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestTour.Repository.Implementation
{
    public class AddCartRepository : IAddCartRepository
    {
        private static Dictionary<int, List<CartItemModel>> userCarts = new Dictionary<int, List<CartItemModel>>();

        public async Task AddToCartAsync(int userId, CartItemModel cartItem)
        {
            if (!userCarts.ContainsKey(userId))
            {
                userCarts[userId] = new List<CartItemModel>();
            }

            var cart = userCarts[userId];
            // Tìm kiếm theo thuộc tính của CustomProduct, ví dụ như ProductCode
            var existingItem = cart.FirstOrDefault(c => c.CustomProduct.ProductCode == cartItem.CustomProduct.ProductCode);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(cartItem);
            }

            await Task.CompletedTask; // Chuyển đổi sang phương thức bất đồng bộ nếu cần
        }
        public async Task RemoveAllFromCartAsync(int userId)
        {
            if (userCarts.ContainsKey(userId))
            {
                userCarts[userId].Clear(); // Xóa tất cả sản phẩm trong giỏ hàng
            }

            await Task.CompletedTask;
        }

        public async Task RemoveFromCartAsync(int userId, string productCode) // Thay đổi tham số từ int sang string
        {
            if (userCarts.ContainsKey(userId))
            {
                var cart = userCarts[userId];
                // Tìm kiếm theo ProductCode
                var existingItem = cart.FirstOrDefault(c => c.CustomProduct.ProductCode == productCode);

                if (existingItem != null)
                {
                    cart.Remove(existingItem);
                }
            }

            await Task.CompletedTask; 
        }

        public async Task<List<CartItemModel>> GetUserCartAsync(int userId)
        {
            if (userCarts.ContainsKey(userId))
            {
                return await Task.FromResult(userCarts[userId]);
            }

            return new List<CartItemModel>();
        }

        public async Task UpdateCartAsync(int userId, List<CartItemModel> updatedCart)
        {
            userCarts[userId] = updatedCart;
            await Task.CompletedTask;
        }
    }
}

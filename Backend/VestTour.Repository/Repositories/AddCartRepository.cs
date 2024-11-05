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
            var existingItem = cart.FirstOrDefault(c =>
                (!cartItem.IsCustom && c.ProductID == cartItem.ProductID) ||
                (cartItem.IsCustom && c.CustomProduct?.ProductCode == cartItem.CustomProduct?.ProductCode)
            );

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(cartItem);
            }

            await Task.CompletedTask;
        }

        public async Task<List<CartItemModel>> GetUserCartAsync(int userId)
        {
            return userCarts.ContainsKey(userId) ? await Task.FromResult(userCarts[userId]) : new List<CartItemModel>();
        }

        public async Task RemoveFromCartAsync(int userId, string productCode)
        {
            if (userCarts.ContainsKey(userId))
            {
                var cart = userCarts[userId];
                var existingItem = cart.FirstOrDefault(c => c.IsCustom && c.CustomProduct.ProductCode == productCode);
                if (existingItem != null)
                {
                    cart.Remove(existingItem);
                }
            }

            await Task.CompletedTask;
        }

        public async Task RemoveAllFromCartAsync(int userId)
        {
            if (userCarts.ContainsKey(userId))
            {
                userCarts[userId].Clear();
            }

            await Task.CompletedTask;
        }

        public async Task UpdateCartAsync(int userId, List<CartItemModel> updatedCart)
        {
            userCarts[userId] = updatedCart;
            await Task.CompletedTask;
        }
    }
}

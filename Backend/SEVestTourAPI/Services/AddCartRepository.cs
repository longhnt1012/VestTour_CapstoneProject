using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public class AddCartRepository : IAddCartRepository
    {
       
        private static Dictionary<int, List<CartItemModel>> userCarts = new Dictionary<int, List<CartItemModel>>();

        public async Task AddToCartAsync(int userId, int productId)
        {
           

            if (!userCarts.ContainsKey(userId))
            {
                userCarts[userId] = new List<CartItemModel>();
            }

            var cart = userCarts[userId];
            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++; 
            }
            else
            {
                cart.Add(new CartItemModel { ProductId = productId, Quantity = 1 });
            }

            await Task.CompletedTask;
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            if (userCarts.ContainsKey(userId))
            {
                var cart = userCarts[userId];
                var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);

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
    }
}

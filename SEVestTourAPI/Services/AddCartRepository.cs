<<<<<<<< Updated upstream:SEVestTourAPI/Services/AddCartRepository.cs
﻿using SEVestTourAPI.Models;
========
﻿using VestTour.Repository.Interface;
using VestTour.Repository.Models;
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/AddCartRepository.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

<<<<<<<< Updated upstream:SEVestTourAPI/Services/AddCartRepository.cs
namespace SEVestTourAPI.Services
{
    public class AddCartRepository : IAddCartRepository
    {
       
========
namespace VestTour.Repository.Implementation
{
    public class AddCartRepository : IAddCartRepository
    {
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/AddCartRepository.cs
        private static Dictionary<int, List<CartItemModel>> userCarts = new Dictionary<int, List<CartItemModel>>();

        public async Task AddToCartAsync(int userId, CartItemModel cartItem)
        {
<<<<<<<< Updated upstream:SEVestTourAPI/Services/AddCartRepository.cs
           

========
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/AddCartRepository.cs
            if (!userCarts.ContainsKey(userId))
            {
                userCarts[userId] = new List<CartItemModel>();
            }

            var cart = userCarts[userId];
            var existingItem = cart.FirstOrDefault(c => c.ProductId == cartItem.ProductId);

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

        // Method to update the cart after changing quantities
        public async Task UpdateCartAsync(int userId, List<CartItemModel> updatedCart)
        {
            userCarts[userId] = updatedCart;
            await Task.CompletedTask;
        }
    }
}

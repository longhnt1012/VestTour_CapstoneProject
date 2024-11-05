using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Repository.Interface
{
    public interface IAddCartRepository
    {
        Task AddToCartAsync(int userId, CartItemModel cartItem);
        Task RemoveFromCartAsync(int userId, string productCode);
        Task<List<CartItemModel>> GetUserCartAsync(int userId);
        Task UpdateCartAsync(int userId, List<CartItemModel> updatedCart);
        Task RemoveAllFromCartAsync(int userId);

    }
}

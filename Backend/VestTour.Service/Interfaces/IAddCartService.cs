using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Interfaces
{
    public interface IAddCartService
    {
        Task AddToCartAsync(int userId, int productId);
        Task RemoveFromCartAsync(int userId, int productId);
        Task<List<CartItemModel>> GetUserCartAsync(int userId);
        Task DecreaseQuantityAsync(int userId, int productId);
        Task IncreaseQuantityAsync(int userId, int productId);
    }
}

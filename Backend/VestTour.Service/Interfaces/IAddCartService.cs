using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Interfaces
{
    public interface IAddCartService
    {
        Task AddToCartAsync(int userId, CustomProductModel customProductModel);
        Task RemoveFromCartAsync(int userId, string productCode);

        Task<CartModel> GetUserCartAsync(int userId);
        Task DecreaseQuantityAsync(int userId, string productCode);
        Task IncreaseQuantityAsync(int userId, string productCode);
        Task ConfirmOrderAsync(int userId);
    }
}

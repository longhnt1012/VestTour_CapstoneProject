using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IAddCartService
    {
        Task AddToCartAsync(int? userId, bool isCustom, int? productId = null, CustomProductModel customProduct = null);
        Task RemoveFromCartAsync(int? userId, string productCode);
        Task ClearCartAsync(int? userId);
        Task<CartModel> GetUserCartAsync(int? userId);
        Task DecreaseQuantityAsync(int? userId, string productCode);
        Task IncreaseQuantityAsync(int? userId, string productCode);
        Task<ServiceResponse<int>> ConfirmOrderAsync(int? userId, string? guestName, string? guestEmail, string? guestAddress, string? guestPhone, decimal deposit, decimal shippingFee, string? deliveryMethod, int storeId, int? voucherId, string? note);
        Task<decimal> GetTotalPriceAsync(int? userId);
    }
}

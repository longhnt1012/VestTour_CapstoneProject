using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Service.Interfaces
{
    public interface IAddCartService
    {
        Task AddToCartAsync(int? userId, bool isCustom, int? productId = null, CustomProductModel customProduct = null);
        Task RemoveFromCartAsync(int? userId, string productCode);
        Task<CartModel> GetUserCartAsync(int? userId);
        Task DecreaseQuantityAsync(int? userId, string productCode);
        Task IncreaseQuantityAsync(int? userId, string productCode);
        Task ConfirmOrderAsync(int? userId, string? guestName, string? guestEmail, string? guestAddress, decimal? deposit, decimal? shippingFee, string? deliverymethod, int? voucherId, int? storeId);
        Task<decimal> GetTotalPriceAsync(int? userId);
    }
}

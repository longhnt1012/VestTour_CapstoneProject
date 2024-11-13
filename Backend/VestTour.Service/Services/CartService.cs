using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Services
{
   public class CartService : ICartService
{
    private readonly Dictionary<string, List<CustomProductModel>> _userCarts;

    public CartService()
    {
        _userCarts = new Dictionary<string, List<CustomProductModel>>();
    }

    public List<CustomProductModel> GetCartByUserId(string userId)
    {
        if (_userCarts.ContainsKey(userId))
        {
            return _userCarts[userId];
        }
        return new List<CustomProductModel>();
    }

    public bool AddToCart(string userId, CustomProductModel product)
    {
        if (!_userCarts.ContainsKey(userId))
        {
            _userCarts[userId] = new List<CustomProductModel>();
        }
        _userCarts[userId].Add(product);
        return true;
    }

    public bool RemoveFromCart(string userId, int index)
    {
        if (_userCarts.ContainsKey(userId) && index >= 0 && index < _userCarts[userId].Count)
        {
            _userCarts[userId].RemoveAt(index);
            return true;
        }
        return false;
    }
}

}

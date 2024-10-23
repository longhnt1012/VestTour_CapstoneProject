using VestTour.Repository.Models;

public interface ICartService
{

    List<CustomProductModel> GetCartByUserId(string userId);

 
     bool AddToCart(string userId, CustomProductModel product);


    bool RemoveFromCart(string userId, int index);

    
   
}

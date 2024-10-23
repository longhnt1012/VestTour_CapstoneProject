﻿using SEVestTourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Services
{
    public interface IAddCartRepository
    {
        Task AddToCartAsync(int userId, int productId); 
        Task RemoveFromCartAsync(int userId, int productId); 
        Task<List<CartItemModel>> GetUserCartAsync(int userId); 

    }
}
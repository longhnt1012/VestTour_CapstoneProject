using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface IProductStyleOptionervice
    {
        Task AddStyleOptionAsync(int productId, int styleOptionId);
        Task<IEnumerable<ProductStyleOptionModel>> GetStyleOptionsByProductIdAsync(int productId);
        Task DeleteStyleOptionsByProductIdAsync(int productId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface IProductStyleOptionRepository
    {
        Task AddProductStyleOptionAsync(ProductStyleOptionModel productStyleOption);
        Task<IEnumerable<ProductStyleOption>> GetByProductIdAsync(int productId);
        Task DeleteByProductIdAsync(int productId);
    }
}

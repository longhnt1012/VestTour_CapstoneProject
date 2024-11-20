using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Repository.Interfaces
{
    public interface ITailorPartnerRepository
    {
        Task<List<TailorPartnerModel>> GetAllTailorPartnersAsync();
        Task<TailorPartnerModel?> GetTailorPartnerByIdAsync(int tailorPartnerId);
        Task<int> AddTailorPartnerAsync(TailorPartnerModel tailorPartner);
        Task UpdateTailorPartnerAsync(int id, TailorPartnerModel tailorPartner);
        Task DeleteTailorPartnerAsync(int tailorPartnerId);
        Task<List<TailorPartnerModel>> GetTailorPartnersByStoreIdAsync(int storeId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface ITailorPartnerService
    {
        Task<ServiceResponse<TailorPartnerModel?>> GetTailorPartnerByIdAsync(int tailorPartnerId);
        Task<ServiceResponse<List<TailorPartnerModel>>> GetAllTailorPartnersAsync();
        Task<ServiceResponse<int>> AddTailorPartnerAsync(TailorPartnerModel tailorPartner);
        Task<ServiceResponse> UpdateTailorPartnerAsync(int id, TailorPartnerModel tailorPartner);
        Task<ServiceResponse> DeleteTailorPartnerAsync(int tailorPartnerId);
        Task<ServiceResponse<List<TailorPartnerModel>>> GetTailorPartnersByStoreIdAsync(int storeId);
        Task<ServiceResponse<TailorPartnerModel>> GetTailorPartnersByUserIdAsync(int userId);
    }
}

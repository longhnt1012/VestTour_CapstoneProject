using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interface
{
    public interface IRegisterService
    {
        Task<string?> RegisterUserAsync(RegisterModel registerModel);
    }
}

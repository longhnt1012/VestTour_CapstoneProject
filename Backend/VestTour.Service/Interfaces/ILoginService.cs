using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;

namespace VestTour.Service.Interfaces
{
    public interface ILoginService
    {
        Task<AuthenticationResponseModel> LoginAsync(LoginModel login);
    }
}

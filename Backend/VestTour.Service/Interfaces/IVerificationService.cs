using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestTour.Service.Interfaces
{
    public interface IVerificationService
    {
        bool VerifyCode(string emailOrPhone, string code);
        void SendVerificationCode(string emailOrPhone);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using VestTour.Service.Interface;

namespace VestTour.Service.Interfaces
{
    public interface IPaymentService
    {
        Task<ServiceResponse<int>> AddNewPaymentAsync(PaymentModel payment);
        Task<ServiceResponse> DeletePaymentAsync(int id);
        Task<ServiceResponse<List<PaymentModel>>> GetAllPaymentsAsync();
        Task<ServiceResponse<PaymentModel>> GetPaymentByIdAsync(int id);
        Task<ServiceResponse> UpdatePaymentAsync(int id, PaymentModel payment);
        Task UpdatePaymentOrderIdAsync(int paymentId, int orderId);
    }
}

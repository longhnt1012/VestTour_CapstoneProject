using VestTour.Repository.Models;

namespace VestTour.Repository.Interface
{
    public interface IPaymentRepository
    {
        public Task<List<PaymentModel>> GetAllPaymentAsync();

        public Task<PaymentModel> GetPaymentByIDAsync(int id);

        public Task<int> AddNewPayment(PaymentModel payment);

        public Task UpdatePayment(int id, PaymentModel payment);

        public Task DeletePayment(int id);
        Task<List<PaymentModel>> GetPaymentsByOrderIdAsync(int orderId);
        Task<List<PaymentModel>> GetPaymentsByUserIdAsync(int userId);
    }
}

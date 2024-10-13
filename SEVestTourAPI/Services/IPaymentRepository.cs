using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public interface IPaymentRepository
    {
        public Task<List<PaymentModel>> GetAllPaymentAsync();

        public Task<PaymentModel> GetPaymentByIDAsync(int id);

        public Task<int> AddNewPayment(PaymentModel payment);

        public Task UpdatePayment(int id, PaymentModel payment);

        public Task DeletePayment(int id);
    }
}

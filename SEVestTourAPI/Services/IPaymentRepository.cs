using VestTour.Repository.Models;

<<<<<<<< Updated upstream:SEVestTourAPI/Services/IPaymentRepository.cs
namespace SEVestTourAPI.Services
========
namespace VestTour.Repository.Interface
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Interfaces/IPaymentRepository.cs
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

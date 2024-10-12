using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;

namespace SEVestTourAPI.Services
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public PaymentRepository(VestTourDbContext context, IMapper mapper) 
        {
        _context= context;
        _mapper= mapper;
        }

        public async Task<int> AddNewPayment(PaymentModel payment)
        {
            var newPayment= _mapper.Map<Payment>(payment);
            _context.Payments!.Add(newPayment);
            _context.SaveChangesAsync();
            return newPayment.PaymentId;
        }

        public async Task DeletePayment(int id)
        {

            var deletePayment= await _context.Payments.FindAsync(id);
            if( deletePayment != null)
            {
                _context.Remove(deletePayment);
                await _context.SaveChangesAsync();

            }

        }

        public async Task<List<PaymentModel>> GetAllPaymentAsync()
        {
            var payments= await _context.Payments!.ToListAsync();
            return _mapper.Map<List<PaymentModel>>(payments);
        }

        public async Task<PaymentModel> GetPaymentByIDAsync(int id)
        {
            var payment = await _context.Payments!.FirstOrDefaultAsync(pa => pa.PaymentId == id);
            return _mapper.Map<PaymentModel>(payment);
        }

        public async Task UpdatePayment(int id, PaymentModel payment)
        {
            if (id == payment.PaymentId)
            {
                var updatePayment = _mapper.Map<Payment>(payment);
                _context.Payments!.Update(updatePayment);
                await _context.SaveChangesAsync();
            }
        }
    }
}

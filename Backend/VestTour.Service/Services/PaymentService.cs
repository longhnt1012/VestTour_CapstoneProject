using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<ServiceResponse<int>> AddNewPaymentAsync(PaymentModel payment)
        {
            var response = new ServiceResponse<int>();
            try
            {
                if (payment == null)
                {
                    response.Success = false;
                    response.Message = "Payment cannot be null.";
                    return response;
                }

                var paymentId = await _paymentRepository.AddNewPayment(payment);
                response.Data = paymentId;
                response.Message = "Payment added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> DeletePaymentAsync(int id)
        {
            var response = new ServiceResponse();
            try
            {
                if (id <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid payment ID.";
                    return response;
                }

                await _paymentRepository.DeletePayment(id);
                response.Message = "Payment deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<PaymentModel>>> GetAllPaymentsAsync()
        {
            var response = new ServiceResponse<List<PaymentModel>>();
            try
            {
                var payments = await _paymentRepository.GetAllPaymentAsync();
                response.Data = payments;
                response.Message = "Payments retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<PaymentModel>> GetPaymentByIdAsync(int id)
        {
            var response = new ServiceResponse<PaymentModel>();
            try
            {
                if (id <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid payment ID.";
                    return response;
                }

                var payment = await _paymentRepository.GetPaymentByIDAsync(id);
                if (payment == null)
                {
                    response.Success = false;
                    response.Message = "Payment not found.";
                    return response;
                }

                response.Data = payment;
                response.Message = "Payment retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdatePaymentAsync(int id, PaymentModel payment)
        {
            var response = new ServiceResponse();
            try
            {
                if (id <= 0 || payment == null || id != payment.PaymentId)
                {
                    response.Success = false;
                    response.Message = "Invalid payment or payment ID.";
                    return response;
                }

                await _paymentRepository.UpdatePayment(id, payment);
                response.Message = "Payment updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
        public async Task UpdatePaymentOrderIdAsync(int paymentId, int orderId)
        {
            var payment = await _paymentRepository.GetPaymentByIDAsync(paymentId);
            if (payment == null)
            {
                throw new Exception("Payment not found.");
            }
            await _paymentRepository.UpdatePaymentOrderId(paymentId, orderId);
        }
        public async Task<ServiceResponse<List<PaymentModel>>> GetPaymentsByOrderIdAsync(int orderId)
        {
            var response = new ServiceResponse<List<PaymentModel>>();
            try
            {
                if (orderId <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid Order ID.";
                    return response;
                }

                var payments = await _paymentRepository.GetPaymentsByOrderIdAsync(orderId);
                response.Data = payments;
                response.Message = payments.Any() ? "Payments retrieved successfully." : "No payments found for the specified Order ID.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<PaymentModel>>> GetPaymentsByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<List<PaymentModel>>();
            try
            {
                if (userId <= 0)
                {
                    response.Success = false;
                    response.Message = "Invalid User ID.";
                    return response;
                }

                var payments = await _paymentRepository.GetPaymentsByUserIdAsync(userId);
                response.Data = payments;
                response.Message = payments.Any() ? "Payments retrieved successfully." : "No payments found for the specified User ID.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


    }
}

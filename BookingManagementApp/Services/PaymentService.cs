using System;
using System.Threading.Tasks;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.Extensions.Logging;

namespace BookingManagementApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IBookingRepository bookingRepository,
            ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment)
        {
            try
            {
                if (payment == null)
                    throw new ArgumentNullException(nameof(payment));

                if (!await ValidatePaymentAsync(payment))
                    throw new InvalidOperationException("Invalid payment information");

                var booking = await _bookingRepository.GetByIdAsync(payment.BookingId);
                if (booking == null)
                    throw new InvalidOperationException("Booking not found");

                // TODO: Integrate with actual payment gateway
                // This is a placeholder implementation
                payment.Status = "Processed";
                payment.ProcessedDate = DateTime.UtcNow;

                return await _paymentRepository.AddAsync(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for booking {BookingId}", payment?.BookingId);
                throw;
            }
        }

        public async Task<bool> ProcessRefundAsync(int paymentId)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(paymentId);
                if (payment == null)
                    throw new InvalidOperationException("Payment not found");

                if (payment.Status == "Refunded")
                    throw new InvalidOperationException("Payment has already been refunded");

                // TODO: Integrate with actual payment gateway for refund processing
                // This is a placeholder implementation
                payment.Status = "Refunded";
                payment.RefundDate = DateTime.UtcNow;

                await _paymentRepository.UpdateAsync(payment);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing refund for payment {PaymentId}", paymentId);
                throw;
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            try
            {
                if (paymentId <= 0)
                    throw new ArgumentException("Invalid payment ID");

                return await _paymentRepository.GetByIdAsync(paymentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payment {PaymentId}", paymentId);
                throw;
            }
        }

        public async Task<Payment> GetPaymentByBookingIdAsync(int bookingId)
        {
            try
            {
                if (bookingId <= 0)
                    throw new ArgumentException("Invalid booking ID");

                return await _paymentRepository.GetByBookingIdAsync(bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payment for booking {BookingId}", bookingId);
                throw;
            }
        }

        public async Task<bool> ValidatePaymentAsync(Payment payment)
        {
            try
            {
                if (payment == null)
                    throw new ArgumentNullException(nameof(payment));

                // Basic validation rules
                if (payment.Amount <= 0)
                    return false;

                if (string.IsNullOrEmpty(payment.PaymentMethod))
                    return false;

                // TODO: Add more validation rules and integrate with payment gateway
                // for card validation, etc.

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating payment for booking {BookingId}", payment?.BookingId);
                throw;
            }
        }
    }
}
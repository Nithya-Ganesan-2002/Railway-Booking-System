using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Services
{
    /// <summary>
    /// Interface for managing payment operations
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Processes a payment for a booking
        /// </summary>
        /// <param name="payment">The payment details</param>
        /// <returns>The processed payment</returns>
        Task<Payment> ProcessPaymentAsync(Payment payment);

        /// <summary>
        /// Processes a refund for a cancelled booking
        /// </summary>
        /// <param name="paymentId">The payment ID</param>
        /// <returns>True if refund was successful, false otherwise</returns>
        Task<bool> ProcessRefundAsync(int paymentId);

        /// <summary>
        /// Gets payment details by ID
        /// </summary>
        /// <param name="paymentId">The payment ID</param>
        /// <returns>The payment details</returns>
        Task<Payment> GetPaymentByIdAsync(int paymentId);

        /// <summary>
        /// Gets payment details for a booking
        /// </summary>
        /// <param name="bookingId">The booking ID</param>
        /// <returns>The payment details</returns>
        Task<Payment> GetPaymentByBookingIdAsync(int bookingId);

        /// <summary>
        /// Validates payment information
        /// </summary>
        /// <param name="payment">The payment details to validate</param>
        /// <returns>True if payment information is valid, false otherwise</returns>
        Task<bool> ValidatePaymentAsync(Payment payment);
    }
}
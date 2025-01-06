using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Repository interface for managing payments
    /// </summary>
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        /// <summary>
        /// Gets payment by booking id
        /// </summary>
        Task<Payment> GetPaymentByBookingIdAsync(int bookingId);

        /// <summary>
        /// Gets all payments for a specific user
        /// </summary>
        Task<IEnumerable<Payment>> GetUserPaymentsAsync(int userId);

        /// <summary>
        /// Gets payments by status
        /// </summary>
        Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(string status);

        /// <summary>
        /// Processes a refund for a payment
        /// </summary>
        Task ProcessRefundAsync(int paymentId);
    }
}
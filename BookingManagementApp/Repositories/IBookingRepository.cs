using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Repository interface for managing bookings
    /// </summary>
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        /// <summary>
        /// Gets all bookings for a specific user
        /// </summary>
        Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId);

        /// <summary>
        /// Gets all bookings for a specific train
        /// </summary>
        Task<IEnumerable<Booking>> GetTrainBookingsAsync(int trainId);

        /// <summary>
        /// Gets bookings for a specific date range
        /// </summary>
        Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Cancels a booking
        /// </summary>
        Task CancelBookingAsync(int bookingId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Services
{
    /// <summary>
    /// Interface for managing booking operations
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Creates a new booking
        /// </summary>
        /// <param name="booking">The booking details</param>
        /// <returns>The created booking</returns>
        Task<Booking> CreateBookingAsync(Booking booking);

        /// <summary>
        /// Cancels an existing booking
        /// </summary>
        /// <param name="bookingId">The booking ID</param>
        /// <returns>True if cancellation was successful, false otherwise</returns>
        Task<bool> CancelBookingAsync(int bookingId);

        /// <summary>
        /// Gets booking details by ID
        /// </summary>
        /// <param name="bookingId">The booking ID</param>
        /// <returns>The booking details</returns>
        Task<Booking> GetBookingByIdAsync(int bookingId);

        /// <summary>
        /// Gets all bookings for a user
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>List of bookings</returns>
        Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId);

        /// <summary>
        /// Gets all bookings for a specific train
        /// </summary>
        /// <param name="trainId">The train ID</param>
        /// <returns>List of bookings</returns>
        Task<IEnumerable<Booking>> GetTrainBookingsAsync(int trainId);

        /// <summary>
        /// Checks if a seat is available for booking
        /// </summary>
        /// <param name="trainId">The train ID</param>
        /// <param name="seatNumber">The seat number</param>
        /// <returns>True if the seat is available, false otherwise</returns>
        Task<bool> IsSeatAvailableAsync(int trainId, int seatNumber);
    }
}
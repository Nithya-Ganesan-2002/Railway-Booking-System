using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Services
{
    /// <summary>
    /// Interface for managing notification operations
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends a notification to a user
        /// </summary>
        /// <param name="notification">The notification details</param>
        /// <returns>True if notification was sent successfully, false otherwise</returns>
        Task<bool> SendNotificationAsync(Notification notification);

        /// <summary>
        /// Gets all notifications for a user
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>List of notifications</returns>
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);

        /// <summary>
        /// Gets a notification by ID
        /// </summary>
        /// <param name="notificationId">The notification ID</param>
        /// <returns>The notification details</returns>
        Task<Notification> GetNotificationByIdAsync(int notificationId);

        /// <summary>
        /// Marks a notification as read
        /// </summary>
        /// <param name="notificationId">The notification ID</param>
        /// <returns>True if marked as read successfully, false otherwise</returns>
        Task<bool> MarkNotificationAsReadAsync(int notificationId);

        /// <summary>
        /// Sends a booking confirmation notification
        /// </summary>
        /// <param name="bookingId">The booking ID</param>
        /// <returns>True if notification was sent successfully, false otherwise</returns>
        Task<bool> SendBookingConfirmationAsync(int bookingId);

        /// <summary>
        /// Sends a booking cancellation notification
        /// </summary>
        /// <param name="bookingId">The booking ID</param>
        /// <returns>True if notification was sent successfully, false otherwise</returns>
        Task<bool> SendBookingCancellationAsync(int bookingId);
    }
}
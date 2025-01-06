using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Repository interface for managing notifications
    /// </summary>
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        /// <summary>
        /// Gets all notifications for a specific user
        /// </summary>
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);

        /// <summary>
        /// Gets unread notifications for a user
        /// </summary>
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId);

        /// <summary>
        /// Marks a notification as read
        /// </summary>
        Task MarkAsReadAsync(int notificationId);

        /// <summary>
        /// Gets notifications by type
        /// </summary>
        Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(string type);
    }
}
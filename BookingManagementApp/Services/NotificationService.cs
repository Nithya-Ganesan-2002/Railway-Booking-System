using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.Extensions.Logging;

namespace BookingManagementApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserAccountRepository _userRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository notificationRepository,
            IBookingRepository bookingRepository,
            IUserAccountRepository userRepository,
            ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendNotificationAsync(Notification notification)
        {
            try
            {
                if (notification == null)
                    throw new ArgumentNullException(nameof(notification));

                var user = await _userRepository.GetByIdAsync(notification.UserId);
                if (user == null)
                    throw new InvalidOperationException("User not found");

                notification.SentDate = DateTime.UtcNow;
                notification.Status = "Sent";

                // TODO: Integrate with actual notification service (email/SMS)
                // This is a placeholder implementation
                await _notificationRepository.AddAsync(notification);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notification to user {UserId}", notification?.UserId);
                throw;
            }
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("Invalid user ID");

                return await _notificationRepository.GetByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notifications for user {UserId}", userId);
                throw;
            }
        }

        public async Task<Notification> GetNotificationByIdAsync(int notificationId)
        {
            try
            {
                if (notificationId <= 0)
                    throw new ArgumentException("Invalid notification ID");

                return await _notificationRepository.GetByIdAsync(notificationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notification {NotificationId}", notificationId);
                throw;
            }
        }

        public async Task<bool> MarkNotificationAsReadAsync(int notificationId)
        {
            try
            {
                var notification = await _notificationRepository.GetByIdAsync(notificationId);
                if (notification == null)
                    throw new InvalidOperationException("Notification not found");

                notification.IsRead = true;
                notification.ReadDate = DateTime.UtcNow;

                await _notificationRepository.UpdateAsync(notification);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification {NotificationId} as read", notificationId);
                throw;
            }
        }

        public async Task<bool> SendBookingConfirmationAsync(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(bookingId);
                if (booking == null)
                    throw new InvalidOperationException("Booking not found");

                var notification = new Notification
                {
                    UserId = booking.UserId,
                    Title = "Booking Confirmation",
                    Message = $"Your booking (ID: {bookingId}) has been confirmed. Train: {booking.TrainId}, Seat: {booking.SeatNumber}",
                    Type = "BookingConfirmation",
                    SentDate = DateTime.UtcNow,
                    Status = "Sent"
                };

                return await SendNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking confirmation for booking {BookingId}", bookingId);
                throw;
            }
        }

        public async Task<bool> SendBookingCancellationAsync(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(bookingId);
                if (booking == null)
                    throw new InvalidOperationException("Booking not found");

                var notification = new Notification
                {
                    UserId = booking.UserId,
                    Title = "Booking Cancellation",
                    Message = $"Your booking (ID: {bookingId}) has been cancelled.",
                    Type = "BookingCancellation",
                    SentDate = DateTime.UtcNow,
                    Status = "Sent"
                };

                return await SendNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending booking cancellation for booking {BookingId}", bookingId);
                throw;
            }
        }
    }
}
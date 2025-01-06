using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.Extensions.Logging;

namespace BookingManagementApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITrainScheduleRepository _trainScheduleRepository;
        private readonly ILogger<BookingService> _logger;

        public BookingService(
            IBookingRepository bookingRepository,
            ITrainScheduleRepository trainScheduleRepository,
            ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _trainScheduleRepository = trainScheduleRepository ?? throw new ArgumentNullException(nameof(trainScheduleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            try
            {
                if (booking == null)
                    throw new ArgumentNullException(nameof(booking));

                // Validate train schedule exists
                var trainSchedule = await _trainScheduleRepository.GetByIdAsync(booking.TrainId);
                if (trainSchedule == null)
                    throw new InvalidOperationException("Train schedule not found");

                // Check seat availability
                if (!await IsSeatAvailableAsync(booking.TrainId, booking.SeatNumber))
                    throw new InvalidOperationException("Selected seat is not available");

                booking.BookingDate = DateTime.UtcNow;
                booking.Status = "Confirmed";

                var createdBooking = await _bookingRepository.AddAsync(booking);
                
                // Update seat availability in train schedule
                await UpdateSeatAvailabilityAsync(booking.TrainId, booking.SeatNumber, false);

                return createdBooking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking for user {UserId} on train {TrainId}", 
                    booking?.UserId, booking?.TrainId);
                throw;
            }
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(bookingId);
                if (booking == null)
                    throw new InvalidOperationException("Booking not found");

                if (booking.Status == "Cancelled")
                    throw new InvalidOperationException("Booking is already cancelled");

                booking.Status = "Cancelled";
                await _bookingRepository.UpdateAsync(booking);

                // Release the seat
                await UpdateSeatAvailabilityAsync(booking.TrainId, booking.SeatNumber, true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling booking {BookingId}", bookingId);
                throw;
            }
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            try
            {
                if (bookingId <= 0)
                    throw new ArgumentException("Invalid booking ID");

                return await _bookingRepository.GetByIdAsync(bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting booking {BookingId}", bookingId);
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("Invalid user ID");

                return await _bookingRepository.GetBookingsByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookings for user {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetTrainBookingsAsync(int trainId)
        {
            try
            {
                if (trainId <= 0)
                    throw new ArgumentException("Invalid train ID");

                return await _bookingRepository.GetBookingsByTrainIdAsync(trainId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bookings for train {TrainId}", trainId);
                throw;
            }
        }

        public async Task<bool> IsSeatAvailableAsync(int trainId, int seatNumber)
        {
            try
            {
                if (trainId <= 0)
                    throw new ArgumentException("Invalid train ID");

                if (seatNumber <= 0)
                    throw new ArgumentException("Invalid seat number");

                var existingBookings = await _bookingRepository.GetBookingsByTrainIdAsync(trainId);
                foreach (var booking in existingBookings)
                {
                    if (booking.SeatNumber == seatNumber && booking.Status != "Cancelled")
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking seat availability for train {TrainId}, seat {SeatNumber}", 
                    trainId, seatNumber);
                throw;
            }
        }

        private async Task UpdateSeatAvailabilityAsync(int trainId, int seatNumber, bool isAvailable)
        {
            var trainSchedule = await _trainScheduleRepository.GetByIdAsync(trainId);
            if (trainSchedule != null)
            {
                if (isAvailable)
                    trainSchedule.AvailableSeats++;
                else
                    trainSchedule.AvailableSeats--;

                await _trainScheduleRepository.UpdateAsync(trainSchedule);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Repository interface for managing train schedules
    /// </summary>
    public interface ITrainScheduleRepository : IGenericRepository<TrainSchedule>
    {
        /// <summary>
        /// Gets available trains for a specific date and route
        /// </summary>
        Task<IEnumerable<TrainSchedule>> GetAvailableTrainsAsync(DateTime date, string source, string destination);

        /// <summary>
        /// Updates seat availability for a train
        /// </summary>
        Task UpdateSeatAvailabilityAsync(int trainId, int seatsToUpdate);

        /// <summary>
        /// Gets trains with available seats
        /// </summary>
        Task<IEnumerable<TrainSchedule>> GetTrainsWithAvailableSeatsAsync(int minimumSeats);
    }
}
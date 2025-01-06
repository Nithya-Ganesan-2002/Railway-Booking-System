using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class TrainScheduleRepository : GenericRepository<TrainSchedule>, ITrainScheduleRepository
    {
        public TrainScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TrainSchedule>> GetAvailableTrainsAsync(DateTime departureDate)
        {
            return await _dbSet.Where(t => 
                t.DepartureTime.Date == departureDate.Date && 
                t.AvailableSeats > 0)
                .ToListAsync();
        }

        public async Task<bool> UpdateAvailableSeatsAsync(int trainId, int seatsToReserve)
        {
            var train = await _dbSet.FindAsync(trainId);
            if (train == null || train.AvailableSeats < seatsToReserve)
                return false;

            train.AvailableSeats -= seatsToReserve;
            await SaveChangesAsync();
            return true;
        }
    }
}
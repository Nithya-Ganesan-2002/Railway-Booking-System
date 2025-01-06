using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId)
        {
            return await _dbSet
                .Include(b => b.TrainSchedule)
                .Include(b => b.Payment)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingWithDetailsAsync(int bookingId)
        {
            return await _dbSet
                .Include(b => b.TrainSchedule)
                .Include(b => b.Payment)
                .Include(b => b.UserAccount)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }
    }
}
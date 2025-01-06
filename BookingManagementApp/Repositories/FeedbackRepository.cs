using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Feedback>> GetUserFeedbackAsync(int userId)
        {
            return await _dbSet
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.FeedbackDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetRecentFeedbackAsync(int count)
        {
            return await _dbSet
                .Include(f => f.UserAccount)
                .OrderByDescending(f => f.FeedbackDate)
                .Take(count)
                .ToListAsync();
        }
    }
}
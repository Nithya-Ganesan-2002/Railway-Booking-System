using System.Threading.Tasks;
using BookingManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<UserAccount> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email == email);
        }
    }
}
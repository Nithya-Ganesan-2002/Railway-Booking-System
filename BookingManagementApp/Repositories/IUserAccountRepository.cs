using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Repository interface for managing user accounts
    /// </summary>
    public interface IUserAccountRepository : IGenericRepository<UserAccount>
    {
        /// <summary>
        /// Gets a user by their username
        /// </summary>
        Task<UserAccount> GetByUsernameAsync(string username);

        /// <summary>
        /// Gets a user by their email
        /// </summary>
        Task<UserAccount> GetByEmailAsync(string email);

        /// <summary>
        /// Validates user credentials
        /// </summary>
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}
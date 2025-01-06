using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Services
{
    /// <summary>
    /// Interface for managing user account operations
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="userAccount">The user account details</param>
        /// <returns>The created user account</returns>
        Task<UserAccount> RegisterAsync(UserAccount userAccount);

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>The authenticated user account or null if authentication fails</returns>
        Task<UserAccount> LoginAsync(string username, string password);

        /// <summary>
        /// Updates user profile information
        /// </summary>
        /// <param name="userAccount">The updated user account details</param>
        /// <returns>The updated user account</returns>
        Task<UserAccount> UpdateProfileAsync(UserAccount userAccount);

        /// <summary>
        /// Gets a user account by ID
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <returns>The user account</returns>
        Task<UserAccount> GetUserByIdAsync(int userId);

        /// <summary>
        /// Gets a user account by username
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns>The user account</returns>
        Task<UserAccount> GetUserByUsernameAsync(string username);
    }
}
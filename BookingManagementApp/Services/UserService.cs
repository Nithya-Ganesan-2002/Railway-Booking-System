using System;
using System.Threading.Tasks;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.Extensions.Logging;

namespace BookingManagementApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserAccountRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserAccountRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserAccount> RegisterAsync(UserAccount userAccount)
        {
            try
            {
                if (userAccount == null)
                    throw new ArgumentNullException(nameof(userAccount));

                var existingUser = await _userRepository.GetByUsernameAsync(userAccount.Username);
                if (existingUser != null)
                    throw new InvalidOperationException("Username already exists");

                // Hash password before saving
                userAccount.Password = HashPassword(userAccount.Password);
                
                return await _userRepository.AddAsync(userAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user {Username}", userAccount?.Username);
                throw;
            }
        }

        public async Task<UserAccount> LoginAsync(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new ArgumentException("Username and password are required");

                var isValid = await _userRepository.ValidateCredentialsAsync(username, password);
                if (!isValid)
                    return null;

                return await _userRepository.GetByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {Username}", username);
                throw;
            }
        }

        public async Task<UserAccount> UpdateProfileAsync(UserAccount userAccount)
        {
            try
            {
                if (userAccount == null)
                    throw new ArgumentNullException(nameof(userAccount));

                var existingUser = await _userRepository.GetByIdAsync(userAccount.UserId);
                if (existingUser == null)
                    throw new InvalidOperationException("User not found");

                // Don't update password if not provided
                if (string.IsNullOrEmpty(userAccount.Password))
                    userAccount.Password = existingUser.Password;
                else
                    userAccount.Password = HashPassword(userAccount.Password);

                return await _userRepository.UpdateAsync(userAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", userAccount?.UserId);
                throw;
            }
        }

        public async Task<UserAccount> GetUserByIdAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("Invalid user ID");

                return await _userRepository.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by ID {UserId}", userId);
                throw;
            }
        }

        public async Task<UserAccount> GetUserByUsernameAsync(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    throw new ArgumentException("Username is required");

                return await _userRepository.GetByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username {Username}", username);
                throw;
            }
        }

        private string HashPassword(string password)
        {
            // TODO: Implement proper password hashing using a secure algorithm
            // For now, this is a placeholder
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            );
        }
    }
}
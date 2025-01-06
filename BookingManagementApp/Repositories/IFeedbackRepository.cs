using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Repository interface for managing feedback
    /// </summary>
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        /// <summary>
        /// Gets all feedback for a specific user
        /// </summary>
        Task<IEnumerable<Feedback>> GetUserFeedbackAsync(int userId);

        /// <summary>
        /// Gets all feedback for a specific booking
        /// </summary>
        Task<IEnumerable<Feedback>> GetBookingFeedbackAsync(int bookingId);

        /// <summary>
        /// Gets feedback by rating
        /// </summary>
        Task<IEnumerable<Feedback>> GetFeedbackByRatingAsync(int rating);

        /// <summary>
        /// Gets unresolved feedback
        /// </summary>
        Task<IEnumerable<Feedback>> GetUnresolvedFeedbackAsync();
    }
}
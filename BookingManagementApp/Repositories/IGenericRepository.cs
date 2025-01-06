using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookingManagementApp.Repositories
{
    /// <summary>
    /// Generic repository interface that defines basic CRUD operations
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its id
        /// </summary>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Finds entities based on a predicate
        /// </summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Removes an entity
        /// </summary>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        Task SaveChangesAsync();
    }
}
﻿namespace LearningSystem.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IRepository<TContext, T>
        where TContext : DbContext
        where T : class
    {
        /// <summary>
        /// Find entity by given key.
        /// </summary>
        /// <param name="id">Entity key.</param>
        /// <returns>Found entity.</returns>
        Task<T> FindByIdAsync(string id);

        /// <summary>
        /// Find entity by given key.
        /// </summary>
        /// <param name="id">Entity key.</param>
        /// <returns>Found entity.</returns>
        Task<T> FindByIdAsync(int id);

        /// <summary>
        /// Get entity as enumerable.
        /// </summary>
        /// <returns>Enumerable entity.</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Get entity by given predicate.
        /// </summary>
        /// <param name="predicate">Predicate for filtering.</param>
        /// <returns>Enumerable entity.</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add asynchronous entity to the database.
        /// </summary>
        /// <param name="entity">Entity for adding.</param>
        /// <returns></returns>
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entity);

        /// <summary>
        /// Check if given entity exist in database, if exist delete the entity.
        /// </summary>
        /// <param name="entity">Entity for deleting.</param>
        void Delete(T entity, int id);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity for updating.</param>
        void Update(T entity);

        /// <summary>
        /// Get details for given entity.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Details();

        Task SaveChangesAsync();

        Task<int> GetCountAsync();
    }
}
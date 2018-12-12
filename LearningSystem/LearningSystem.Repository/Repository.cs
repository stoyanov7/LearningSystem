namespace LearningSystem.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Generic class to create an abstraction layer between the data access layer and the business logic layer.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public class Repository<TContext, T> : IRepository<TContext, T>
        where TContext : DbContext
        where T : class 
    {
        private readonly IUnitOfWork<TContext> unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        public Repository(IUnitOfWork<TContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Add asynchronous entity to the database.
        /// </summary>
        /// <param name="entity">Entity for adding.</param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await this.unitOfWork
                .Context
                .Set<T>()
                .AddAsync(entity);

            await this.unitOfWork.CommitAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await this.unitOfWork
                .Context
                .Set<T>()
                .AddRangeAsync(entity);

            await this.unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Find entity by given key.
        /// </summary>
        /// <param name="id">Entity key.</param>
        /// <returns>Found entity.</returns>
        public async Task<T> FindByIdAsync(string id) 
            => await this.unitOfWork
                .Context
                .Set<T>()
                .FindAsync(id);

        /// <summary>
        /// Find entity by given key.
        /// </summary>
        /// <param name="id">Entity key.</param>
        /// <returns>Found entity.</returns>
        public async Task<T> FindByIdAsync(int id) 
            => await this.unitOfWork
                .Context
                .Set<T>()
                .FindAsync(id);

        /// <summary>
        /// Check if given entity exist in database, if exist delete the entity.
        /// </summary>
        /// <param name="entity">Entity for deleting.</param>
        public void Delete(T entity)
        {
            var existing = this.unitOfWork
                .Context
                .Set<T>()
                .Find(entity);

            if (existing != null)
            {
                this.unitOfWork
                    .Context
                    .Set<T>()
                    .Remove(existing);

                this.unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Get entity as enumerable.
        /// </summary>
        /// <returns>Enumerable entity.</returns>
        public IEnumerable<T> Get()
            => this.unitOfWork
                .Context
                .Set<T>()
                .AsEnumerable<T>();

        /// <summary>
        /// Get entity by given predicate.
        /// </summary>
        /// <param name="predicate">Predicate for filtering.</param>
        /// <returns>Enumerable entity.</returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) 
            => this.unitOfWork
                .Context
                .Set<T>()
                .Where(predicate)
                .AsEnumerable<T>();

        /// <summary>
        /// Get details for given entity.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Details() 
            => this.unitOfWork
                .Context
                .Set<T>();

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity for updating.</param>
        public void Update(T entity)
        {
            this.unitOfWork
                .Context
                .Entry(entity)
                .State = EntityState.Modified;

            this.unitOfWork
                .Context
                .Set<T>()
                .Attach(entity);
        }

        public Task SaveChangesAsync() => this.unitOfWork.CommitAsync();
    }
}
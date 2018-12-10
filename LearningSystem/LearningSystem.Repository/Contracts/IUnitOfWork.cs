namespace LearningSystem.Repository.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// Getter for context.
        /// </summary>
        TContext Context { get; }

        /// <summary>
        /// Asynchronous save changes to this context.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task CommitAsync();
    }
}
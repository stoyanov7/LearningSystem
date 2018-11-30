namespace LearningSystem.Repository.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Data;

    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Getter for context.
        /// </summary>
        LearningSystemContext Context { get; }

        /// <summary>
        /// Asynchronous save changes to this context.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task CommitAsync();
    }
}
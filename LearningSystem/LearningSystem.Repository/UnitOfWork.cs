namespace LearningSystem.Repository
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{T}"/> class.
        /// </summary>
        public UnitOfWork(TContext context) => this.Context = context;

        /// <summary>
        /// Getter for context.
        /// </summary>
        public TContext Context { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                this.Context.Dispose();
            }

            // Free any unmanaged objects here.
            this.disposed = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Releases the allocated resources for this context. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Asynchronous save all changes made in this context to the database.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        public async Task CommitAsync() => await this.Context.SaveChangesAsync();
    }
}
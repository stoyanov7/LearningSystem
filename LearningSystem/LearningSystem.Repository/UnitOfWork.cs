namespace LearningSystem.Repository
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;

    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork(LearningSystemContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Getter for context.
        /// </summary>
        public LearningSystemContext Context { get; }

        /// <inheritdoc />
        /// <summary>
        /// Releases the allocated resources for this context. 
        /// </summary>
        public void Dispose() => this.Context.Dispose();

        /// <summary>
        /// Asynchronous save all changes made in this context to the database.
        /// </summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        public async Task CommitAsync() => await this.Context.SaveChangesAsync();
    }
}
namespace LearningSystem.Repository
{
    using System.Threading.Tasks;
    using Contracts;
    using Data;

    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(LearningSystemContext context)
        {
            this.Context = context;
        }

        public LearningSystemContext Context { get; }

        public void Dispose() => this.Context.Dispose();

        public async Task CommitAsync() => await this.Context.SaveChangesAsync();
    }
}
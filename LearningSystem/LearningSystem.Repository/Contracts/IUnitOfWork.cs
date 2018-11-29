namespace LearningSystem.Repository.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Data;

    public interface IUnitOfWork : IDisposable
    {
        LearningSystemContext Context { get; }

        Task CommitAsync();
    }
}
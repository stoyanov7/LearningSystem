namespace LearningSystem.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        void Delete(T entity);

        void Update(T entity);

        IQueryable<T> Details();
    }
}
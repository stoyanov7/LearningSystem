namespace LearningSystem.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(T entity)
        {
            await this.unitOfWork
                .Context
                .Set<T>()
                .AddAsync(entity);

            await this.unitOfWork.CommitAsync();
        }

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

        public IEnumerable<T> Get()
            => this.unitOfWork
                .Context
                .Set<T>()
                .AsEnumerable<T>();

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) 
            => this.unitOfWork
                .Context
                .Set<T>()
                .Where(predicate)
                .AsEnumerable<T>();

        public IQueryable<T> Details() 
            => this.unitOfWork
                .Context
                .Set<T>();


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
    }
}
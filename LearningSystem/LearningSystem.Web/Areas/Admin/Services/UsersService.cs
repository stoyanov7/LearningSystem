namespace LearningSystem.Web.Areas.Admin.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using LearningSystem.Models.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly LearningSystemContext context;

        public UsersService(LearningSystemContext context) => this.context = context;

        public IQueryable<TModel> All<TModel>() => this.By<TModel>();

        public async Task<TModel> Details<TModel>(string id)
            => await this.By<TModel>(x => x.Id == id)
                .FirstOrDefaultAsync();

        public async Task<ApplicationUser> Find(string id)
            => await this.context
                .Users
                .FindAsync(id);

        private IQueryable<TModel> By<TModel>(Expression<Func<ApplicationUser, bool>> predicate = null)
            => this.context
                .Users
                .AsQueryable()
                .Where(predicate ?? (i => true))
                .ProjectTo<TModel>();
    }
}
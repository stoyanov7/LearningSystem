namespace LearningSystem.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Models.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly LearningSystemContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(LearningSystemContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get all users by entity key, except the current user.
        /// </summary>
        /// <typeparam name="TModel">Entity type.</typeparam>
        /// <returns>All records.</returns>
        public async Task<IEnumerable<TModel>> All<TModel>(ClaimsPrincipal user)
        {
            var currentUser = await this.userManager
                .GetUserAsync(user);

            return await this.By<TModel>(x => x.Id != currentUser.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Get details by entity key.
        /// </summary>
        /// <typeparam name="TModel">Entity type.</typeparam>
        /// <param name="id">Entity key.</param>
        /// <returns>All details for record.</returns>
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
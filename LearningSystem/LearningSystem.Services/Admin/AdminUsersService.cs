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
    using Repository.Contracts;

    /// <summary>
    /// Service to entering users data.
    /// </summary>
    public class AdminUsersService : IAdminUsersService
    {
        private readonly IRepository<LearningSystemContext, ApplicationUser> context;
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminUsersService"/> class.
        /// </summary>
        public AdminUsersService(IRepository<LearningSystemContext, ApplicationUser> context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get all users by entity key, except the current user.
        /// </summary>
        /// <typeparam name="TModel">Entity type.</typeparam>
        /// <returns>All records.</returns>
        public async Task<IEnumerable<TModel>> AllUsersAsync<TModel>(ClaimsPrincipal user)
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
        public async Task<TModel> UserDetailsAync<TModel>(string id)
            => await this.By<TModel>(x => x.Id == id)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Get application user by id;
        /// </summary>
        /// <param name="id">Application user id</param>
        /// <returns>Application user</returns>
        public async Task<ApplicationUser> FindUserAsync(string id)
            => await this.context.FindByIdAsync(id);

        private IQueryable<TModel> By<TModel>(Expression<Func<ApplicationUser, bool>> predicate = null)
            => this.context
                .Get()
                .AsQueryable()
                .Where(predicate ?? (i => true))
                .ProjectTo<TModel>();
    }
}
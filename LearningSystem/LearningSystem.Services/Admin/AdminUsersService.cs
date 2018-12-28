namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminUsersService"/> class.
        /// </summary>
        public AdminUsersService(IRepository<LearningSystemContext, ApplicationUser> context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
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

            var repositoryUser = this.context.Details().Where(x => x.Id != currentUser.Id);
            var model = this.mapper.Map<IEnumerable<TModel>>(repositoryUser);

            return model;
        }

        /// <summary>
        /// Get details by entity key.
        /// </summary>
        /// <typeparam name="TModel">Entity type.</typeparam>
        /// <param name="id">Entity key.</param>
        /// <returns>All details for record.</returns>
        public async Task<TModel> UserDetailsAync<TModel>(string id)
        {
            var user = await this.context
                .Details()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var model = this.mapper.Map<TModel>(user);

            return model;
        }

        /// <summary>
        /// Get application user by id;
        /// </summary>
        /// <param name="id">Application user id</param>
        /// <returns>Application user</returns>
        public async Task<ApplicationUser> FindUserAsync(string id) 
            => await this.context.FindByIdAsync(id);
    }
}
namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Models.Identity;

    public interface IUsersService
    {
        /// <summary>
        /// Get all users by entity key, except the current user.
        /// </summary>
        /// <typeparam name="TModel">Entity type.</typeparam>
        /// <returns>All records.</returns>
        Task<IEnumerable<TModel>> All<TModel>(ClaimsPrincipal user);

        /// <summary>
        /// Get details by entity key.
        /// </summary>
        /// <typeparam name="TModel">Entity type.</typeparam>
        /// <param name="id">Entity key.</param>
        /// <returns>All details for record.</returns>
        Task<TModel> Details<TModel>(string id);

        /// <summary>
        /// Get application user by id;
        /// </summary>
        /// <param name="id">Application user id</param>
        /// <returns>Application user</returns>
        Task<ApplicationUser> FindAsync(string id);
    }
}
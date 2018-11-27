namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Models.Identity;

    public interface IUsersService
    {
        Task<IEnumerable<TModel>> All<TModel>(ClaimsPrincipal user);

        Task<TModel> Details<TModel>(string id);

        Task<ApplicationUser> Find(string id);
    }
}
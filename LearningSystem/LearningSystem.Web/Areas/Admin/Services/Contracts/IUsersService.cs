namespace LearningSystem.Web.Areas.Admin.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Models.Identity;

    public interface IUsersService
    {
        IQueryable<TModel> All<TModel>();

        Task<TModel> Details<TModel>(string id);

        Task<ApplicationUser> Find(string id);
    }
}
namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminLecturersService
    {
        Task<IEnumerable<TModel>> GetAllLecturersAsync<TModel>();
    }
}
namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILecturersService
    {
        Task<IEnumerable<TModel>> GetAllLecturers<TModel>();
    }
}
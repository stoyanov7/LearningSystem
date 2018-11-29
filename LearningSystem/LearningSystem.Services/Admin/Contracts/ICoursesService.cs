namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        Task AddCourseAsync<TModel>(TModel model);

        IEnumerable<TModel> All<TModel>();

        Task<TModel> DetailsAsync<TModel>(int id);
    }
}
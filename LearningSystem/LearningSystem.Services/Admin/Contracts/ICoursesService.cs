namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICoursesService
    {
        Task<Course> AddCourseAsync<TModel>(TModel model);

        IEnumerable<TModel> All<TModel>();

        Task<TModel> DetailsAsync<TModel>(int id);
    }
}
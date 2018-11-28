namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.Admin;

    public interface ICoursesService
    {
        Task AddCourseAsync(CreateCourseBindingModel model);

        IEnumerable<TModel> All<TModel>();

        Task<TModel> Details<TModel>(int id);
    }
}
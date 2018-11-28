namespace LearningSystem.Services.Admin.Contracts
{
    using System.Threading.Tasks;
    using ViewModels.Admin;

    public interface ICoursesService
    {
        Task AddCourseAsync(CreateCourseBindingModel model);
    }
}
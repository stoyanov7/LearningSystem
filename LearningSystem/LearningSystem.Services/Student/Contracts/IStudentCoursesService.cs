namespace LearningSystem.Services.Student.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentCoursesService
    {
        Task<TModel> GetCourseAsync<TModel>(int courseId);

        Task<IEnumerable<TModel>> GetAllCoursesAsync<TModel>();

    }
}
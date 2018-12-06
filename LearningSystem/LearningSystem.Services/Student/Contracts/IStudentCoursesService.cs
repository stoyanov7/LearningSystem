namespace LearningSystem.Services.Student.Contracts
{
    using System.Threading.Tasks;

    public interface IStudentCoursesService
    {
        Task<TModel> GetCourseAsync<TModel>(int courseId);

        Task<TModel> GetAllCoursesAsync<TModel>();
    }
}
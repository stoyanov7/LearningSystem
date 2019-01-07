namespace LearningSystem.Services.Student.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentsService
    {
        Task<IEnumerable<TModel>> FindAsync<TModel>(string searchText);

        Task<IEnumerable<TModel>> EnrolledCourses<TModel>(string studentId);

        Task<bool> IsUserEnrolledInCourse(string studentId, int courseId);
    }
}
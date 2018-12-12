namespace LearningSystem.Services.Student.Contracts
{
    using System.Threading.Tasks;

    public interface IStudentCourseInstancesService
    {
        Task<TModel> GetCourseInstancesAsync<TModel>(int courseId);
    }
}
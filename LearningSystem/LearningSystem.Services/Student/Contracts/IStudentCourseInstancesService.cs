namespace LearningSystem.Services.Student.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentCourseInstancesService
    {
        Task<TModel> GetCourseInstancesAsync<TModel>(int courseId);

        Task<IEnumerable<TModel>> GetCourseInstancesAsync<TModel>(string searchText);
    }
}
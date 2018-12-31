namespace LearningSystem.Services.Student.Contracts
{
    using System.Threading.Tasks;

    public interface IStudentQuestionsService
    {
        Task<TModel> GetCourseInstanceAsync<TModel>(string questionSlug);
    }
}
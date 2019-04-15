namespace LearningSystem.Services.Student.Contracts
{
    using System.Threading.Tasks;
    using Models;

    public interface IStudentQuestionsService
    {
        Task<TModel> GetCourseInstanceAsync<TModel>(string questionSlug);

        void AddQuestion(CreateQuestionBindingModel model);

        QuestionPage GetQuestionPage(string questionSlug);
    }
}
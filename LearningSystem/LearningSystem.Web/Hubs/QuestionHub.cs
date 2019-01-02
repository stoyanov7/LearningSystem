namespace LearningSystem.Web.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using Models.CourseInstance;
    using Services.Student;
    using Services.Student.Contracts;

    public class QuestionHub : Hub
    {
        private readonly IStudentQuestionsService questionsService;

        public QuestionHub(IStudentQuestionsService questionsService)
        {
            this.questionsService = questionsService;
        }

        public async Task JoinGroup(string groupName)
        {
            var course = await this.questionsService
                .GetCourseInstanceAsync<CourseInstanceQuestionsViewModel>(groupName);

            if (course == null)
            {
                await this.Clients.Caller.SendAsync("group-rejected", "There is no such group");
            }
            else
            {
                var questionPage = this.questionsService.GetQuestionPage(groupName);

                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
                await this.Clients.Caller.SendAsync("group-accept", course.Name, course.Slug, questionPage);
            }
        }

        public async Task AskQuestion(string groupName, string question, string username)
        {
            var model = new CreateQuestionBindingModel
            {
                QuestionSlug = groupName,
                QuestionText = question,
                Username = username
            };

            this.questionsService.AddQuestion(model);
            await this.Clients.Group(groupName).SendAsync("receive-question", model);
        }
    }
}
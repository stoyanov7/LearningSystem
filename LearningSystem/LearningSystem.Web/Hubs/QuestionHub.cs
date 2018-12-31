namespace LearningSystem.Web.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using Models.CourseInstance;
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
            var course = await this.questionsService.GetCourseInstanceAsync<CourseInstanceQuestionsViewModel>(groupName);

            if (course == null)
            {
                await this.Clients.Caller.SendAsync("group-rejected", "There is no such group");
            }
            else
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
                await this.Clients.Caller.SendAsync("group-accept", course.Name, course.Slug);
            }
        }

        public async Task AskQuestion(string groupName, string question)
        {
            await this.Clients.Group(groupName).SendAsync("receive-question", question);
        }
    }
}
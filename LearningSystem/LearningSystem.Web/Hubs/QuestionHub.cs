namespace LearningSystem.Web.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class QuestionHub : Hub
    {
        public async Task PostQuestion(string user, string question)
        {
            await this.Clients.All.SendAsync("showQuestion", user, question);
        }
    
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
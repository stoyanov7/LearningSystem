namespace LearningSystem.Web.Areas.Admin.Models.ViewModels
{
    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsLecturer { get; set; }

        public bool IsAdmin { get; set; }
    }
}
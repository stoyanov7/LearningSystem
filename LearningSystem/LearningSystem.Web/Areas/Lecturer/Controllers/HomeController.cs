namespace LearningSystem.Web.Areas.Lecturer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : LecturerController
    {
        public IActionResult Index() => this.View();
    }
}
namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BlogController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
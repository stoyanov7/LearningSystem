namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Blog.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Contracts;
    using Services.Blog.Contracts;

    public class HomeController : AdminController
    {
        private readonly IBlogArticleService blogArticleService;
        private readonly IAdminCourseInstancesService courseInstancesService;

        public HomeController(IBlogArticleService blogArticleService,
            IAdminCourseInstancesService courseInstancesService)
        {
            this.blogArticleService = blogArticleService;
            this.courseInstancesService = courseInstancesService;
        }

        public async Task<IActionResult> Index()
        {
            var home = new AdminHomeViewModel
            {
                Articles = this.blogArticleService.AllArticles<AdminArticlesViewModel>(),
                Courses = await this.courseInstancesService.AllCourseInstancesAsync<AdminCoursesViewModel>()
            };

            return this.View(home);
        }
    }
}
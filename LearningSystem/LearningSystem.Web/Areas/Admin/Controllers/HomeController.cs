namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Blog.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Blog.Contracts;

    public class HomeController : AdminController
    {
        private readonly IBlogArticleService blogArticleService;

        public HomeController(IBlogArticleService blogArticleService)
        {
            this.blogArticleService = blogArticleService;
        }

        public IActionResult Index()
        {
            var home = new AdminHomeViewModel
            {
                Articles = this.blogArticleService.AllArticles<BlogArticleViewModel>()
            };

            return this.View(home);
        }
    }
}
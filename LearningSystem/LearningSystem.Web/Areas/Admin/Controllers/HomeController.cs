namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Blog.Models;
    using Microsoft.AspNetCore.Mvc;
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
            var articles = this.blogArticleService.AllArticles<BlogArticleViewModel>();

            return this.View(articles);
        }
    }
}
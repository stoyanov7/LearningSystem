namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Blog.Contracts;

    public class HomeController : BlogController
    {
        private readonly IBlogArticleService blogArticleService;

        public HomeController(IBlogArticleService blogArticleService)
        {
            this.blogArticleService = blogArticleService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new ArticleListingViewModel
            {
                Articles = this.blogArticleService.AllArticles<BlogArticleViewModel>(page),
                TotalArticles = await this.blogArticleService.TotalAsync(),
                CurrentPage = page
            };

            return this.View(model);
        }

        public IActionResult Details(int id)
        {
            return this.View();
        }
    }
}
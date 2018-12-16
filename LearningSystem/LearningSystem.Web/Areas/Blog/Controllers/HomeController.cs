namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Blog.Contracts;

    [Area("Blog")]
    [AllowAnonymous]
    public class HomeController : Controller
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

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.blogArticleService
                .ArticleDetailsAsync<ArticleDetailsViewModel>(id);

            return this.View(model);
        }
    }
}
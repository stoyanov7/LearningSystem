namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Blog.Contracts;
    using Services.Html.Contracts;

    public class ArticlesController : BlogController
    {
        private readonly IBlogArticleService blogArticleService;
        private readonly IHtmlService htmlService;

        public ArticlesController(IBlogArticleService blogArticleService,
            IHtmlService htmlService)
        {
            this.blogArticleService = blogArticleService;
            this.htmlService = htmlService;
        }

        [HttpGet]
        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(PublishArticleBindingModel model)
        {
            model.Content = this.htmlService.Sanitize(model.Content);
            var authorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.blogArticleService.CreateArticleAsync(model, authorId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
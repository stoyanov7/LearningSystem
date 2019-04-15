namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services.Blog.Contracts;
    using Services.Html.Contracts;
    using Utilities.Constants;

    [Area(BlogConstants.BlogArea)]
    [Authorize(Roles = BlogConstants.BloggerAndAdministrator)]
    public class ArticlesController : Controller
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublishArticleBindingModel model)
        {
            model.Content = this.htmlService.Sanitize(model.Content);
            var authorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.blogArticleService.CreateArticleAsync(model, authorId);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var article = await this.blogArticleService
                .FindByIdAsync<BlogArticleViewModel>(id);

            if (article == null)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogArticleViewModel model)
        {
            if (id != model.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.blogArticleService.Edit(model.Id, model.Title, model.Content);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.blogArticleService.ExistsAsync(model.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this.blogArticleService.ExistsAsync(id))
            {
                return this.RedirectToAction("Index", "Home");
            }

            var article = await this.blogArticleService.FindByIdAsync<BlogArticleViewModel>(id);

            return this.View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (!await this.blogArticleService.ExistsAsync(id))
            {
                return this.RedirectToAction("Index", "Home");
            }

            await this.blogArticleService.Delete(id);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
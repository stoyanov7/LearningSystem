namespace LearningSystem.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;
    using Utilities.Common;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly IRepository<LearningSystemContext, Article> repository;
        private readonly IMapper mapper;

        public BlogArticleService(IRepository<LearningSystemContext, Article> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<TModel> AllArticles<TModel>()
        {
            var articles = this.repository
                .Get()
                .AsQueryable()
                .Include(x => x.Author)
                .OrderByDescending(a => a.PublishDate);

            var model = this.mapper.Map<IEnumerable<TModel>>(articles);

            return model;
        }

        public IEnumerable<TModel> AllArticles<TModel>(int page = 1)
        {
            var articles = this.repository
                .Get()
                .AsQueryable()
                .Include(x => x.Author)
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * 25)
                .Take(25);

            var model = this.mapper.Map<IEnumerable<TModel>>(articles);

            return model;
        } 

        public async Task CreateArticleAsync<TModel>(TModel model, string authorId)
        {
            CoreValidator.EnsureNotNull(model, "The article is null");
            var article = this.mapper.Map<Article>(model);
            article.AuthorId = authorId;
            article.PublishDate = DateTime.UtcNow;

            await this.repository.AddAsync(article);
        }

        public async Task<int> TotalAsync() => await this.repository.GetCountAsync();

        public async Task<TModel> ArticleDetailsAsync<TModel>(int id)
        {
            var article = await this.repository
                .Details()
                .Include(a => a.Author)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            var model = this.mapper.Map<TModel>(article);

            return model;
        }

        public async Task<TModel> FindByIdAsync<TModel>(int? id)
        {
            var article = await this.repository
                .Details()
                .SingleOrDefaultAsync(x => x.Id == id);

            var model = this.mapper.Map<TModel>(article);

            return model;
        }

        public async Task Edit(int id, string title, string content)
        {
            var article = await this.repository
                .Details()
                .SingleOrDefaultAsync(g => g.Id == id);

            article.Title = title;
            article.Content = content;

            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await this.repository
                .Get()
                .AsQueryable()
                .AnyAsync(u => u.Id == id);
    }
}
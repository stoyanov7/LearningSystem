namespace LearningSystem.Services.Blog
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
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

        public async Task CreateAsync<TModel>(TModel model, string authorId)
        {
            CoreValidator.EnsureNotNull(model, "The article is null");
            var article = this.mapper.Map<Article>(model);
            article.AuthorId = authorId;
            article.PublishDate = DateTime.UtcNow;

            await this.repository.AddAsync(article);
        }
    }
}
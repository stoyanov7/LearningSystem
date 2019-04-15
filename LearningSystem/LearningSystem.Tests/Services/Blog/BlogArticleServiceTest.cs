namespace LearningSystem.Tests.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using LearningSystem.Services.Blog;
    using Mocks;
    using Models;
    using Moq;
    using Repository.Contracts;
    using Web.Areas.Blog.Models;
    using Xunit;

    public class BlogArticleServiceTest
    {
        private readonly Mock<IRepository<LearningSystemContext, Article>> blogRepositoryMock;
        private readonly LearningSystemContext context;
        private readonly IMapper mapper;

        public BlogArticleServiceTest()
        {
            this.blogRepositoryMock = new Mock<IRepository<LearningSystemContext, Article>>();
            this.context = LearningSystemContextMock.GetContext();
            this.mapper = AutoMapperMock.GetMapper();
        }

        [Fact]
        public async Task CreateArticleAsync_WithNullArticle_ShouldThrowException()
        {
            // Arrange
            Article article = null;
            var servise = new BlogArticleService(null, null);

            // Act
            var result = servise.CreateArticleAsync(article, null);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => result);
        }

        [Fact]
        public async Task AllArticles_WithFewArticles_ShouldReturnAllArticles()
        {
            // Arrange
            this.context.Articles.Add(new Article { Id = 1, Title = "First article" });
            this.context.Articles.Add(new Article { Id = 2, Title = "Second article" });
            this.context.Articles.Add(new Article { Id = 3, Title = "Third article" });
            await this.context.SaveChangesAsync();

            this.blogRepositoryMock
                .Setup(m => m.Get())
                .Returns(this.context.Articles)
                .Verifiable();

            var service = new BlogArticleService(this.blogRepositoryMock.Object, this.mapper);

            // Act
            var result = service.AllArticles<BlogArticleViewModel>();

            // Assert
            this.blogRepositoryMock.Verify();
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Equal(new[] { 1, 2, 3 }, result.Select(c => c.Id).ToArray());
        }

        [Fact]
        public async Task TotalAsync_ShouldReturnCorrectResult()
        {
            // Arrange
            this.blogRepositoryMock
                .Setup(x => x.GetCountAsync())
                .ReturnsAsync(1)
                .Verifiable();

            var service = new BlogArticleService(this.blogRepositoryMock.Object, null);

            // Act
            var result = await service.TotalAsync();

            // Assert
            this.blogRepositoryMock.Verify();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ArticleDetailsAsync()
        {
            // Arrange
            this.context.Articles.Add(new Article { Id = 1, Title = "First article" });
            await this.context.SaveChangesAsync();

            this.blogRepositoryMock
                .Setup(x => x.Details())
                .Returns(this.context.Articles)
                .Verifiable();

            var service = new BlogArticleService(this.blogRepositoryMock.Object, this.mapper);

            // Act
            var result = service.ArticleDetailsAsync<ArticleDetailsViewModel>(1);

            // Assert
            Assert.NotNull(result);
            await Assert.IsAssignableFrom<Task<ArticleDetailsViewModel>>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("First article", result.Result.Title);
        }
    }
}
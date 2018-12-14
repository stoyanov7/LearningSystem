namespace LearningSystem.Services.Blog.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        IEnumerable<TModel> AllArticles<TModel>(int page = 1);

        Task CreateArticleAsync<TModel>(TModel model, string authorId);

        Task<int> TotalAsync();
    }
}
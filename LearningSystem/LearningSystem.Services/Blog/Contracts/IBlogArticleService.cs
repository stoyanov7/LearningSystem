namespace LearningSystem.Services.Blog.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        IEnumerable<TModel> AllArticles<TModel>();

        IEnumerable<TModel> AllArticles<TModel>(int page = 1);

        Task CreateArticleAsync<TModel>(TModel model, string authorId);

        Task<int> TotalAsync();

        Task<TModel> ArticleDetailsAsync<TModel>(int id);

        Task<TModel> FindByIdAsync<TModel>(int? id);

        Task Edit(int id, string title, string content);

        Task Delete(int id);

        Task<bool> ExistsAsync(int id);
    }
}
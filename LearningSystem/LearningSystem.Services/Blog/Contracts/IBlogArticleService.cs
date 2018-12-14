namespace LearningSystem.Services.Blog.Contracts
{
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        Task CreateAsync<TModel>(TModel model, string authorId);
    }
}
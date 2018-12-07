namespace LearningSystem.Services.Admin.Contracts
{
    using System.Threading.Tasks;

    public interface ICourseInstancesService
    {
        Task<int> Create<TModel>(TModel model);

        Task<TModel> DetailsAsync<TModel>(int id);
    }
}
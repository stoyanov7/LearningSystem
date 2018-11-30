namespace LearningSystem.Services.Admin.Contracts
{
    using System.Threading.Tasks;

    public interface ICourseInstancesService
    {
        Task Create<TModel>(TModel model);
    }
}
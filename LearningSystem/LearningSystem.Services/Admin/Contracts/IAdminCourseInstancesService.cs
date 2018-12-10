namespace LearningSystem.Services.Admin.Contracts
{
    using System.Threading.Tasks;

    public interface IAdminCourseInstancesService
    {
        Task<int> CreateCourseIntancesAsync<TModel>(TModel model);

        Task<TModel> CourseInstancesDetailsAsync<TModel>(int id);
    }
}
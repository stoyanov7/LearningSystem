namespace LearningSystem.Services.Lecturer.Contracts
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface ILecturerCourseInstancesService
    {
        Task<TModel> DetailsAsync<TModel>(int id);

        Task<TModel> PrepareInstanceForEditingAsync<TModel>(int id);

        Task EditAsync(int id, ClaimsPrincipal user, string name, string description, DateTime startDate,
            DateTime endDate);

        Task<bool> InstanceExist(int id);

        Task AddLecture<TModel>(int id, TModel model);
    }
}
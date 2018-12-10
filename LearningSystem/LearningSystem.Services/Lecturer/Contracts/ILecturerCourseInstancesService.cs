namespace LearningSystem.Services.Lecturer.Contracts
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface ILecturerCourseInstancesService
    {
        Task<TModel> CourseInstanceDetailsAsync<TModel>(int id);

        Task<TModel> PrepareCourseInstanceForEditingAsync<TModel>(int id);

        Task EditCourseInstanceAsync(int id, ClaimsPrincipal user, string name, string description, DateTime startDate,
            DateTime endDate);

        Task<bool> IsCourseInstanceExist(int id);

        Task AddLecture<TModel>(int id, TModel model);
    }
}
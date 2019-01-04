namespace LearningSystem.Services.Student.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IStudentCourseInstancesService
    {
        Task<TModel> GetCourseInstancesAsync<TModel>(int courseId);

        Task<IEnumerable<TModel>> GetCourseInstancesAsync<TModel>(string searchText);

        IEnumerable<SelectListItem> GetCoursesForDropdownList();
    }
}
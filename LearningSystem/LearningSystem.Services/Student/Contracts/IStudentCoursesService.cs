namespace LearningSystem.Services.Student.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IStudentCoursesService
    {
        IEnumerable<SelectListItem> GetCoursesForDropdownList();

        Task<TModel> GetCourseAsync<TModel>(int courseId);

        Task<IEnumerable<TModel>> GetAllCoursesAsync<TModel>();

    }
}
namespace LearningSystem.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICoursesService
    {
        /// <summary>
        /// Add new course to the database.
        /// </summary>
        /// <typeparam name="TModel">Course model.</typeparam>
        /// <param name="model">Course for adding.</param>
        /// <returns>Return the mapped course.</returns>
        Task<Course> AddCourseAsync<TModel>(TModel model);

        /// <summary>
        /// Get all courses.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns>List with all courses.</returns>
        IEnumerable<TModel> All<TModel>();

        /// <summary>
        /// Get details for course by given key.
        /// </summary>
        /// <typeparam name="TModel">Model of course.</typeparam>
        /// <param name="id">Course key.</param>
        /// <returns>Mapped information for the course.</returns>
        Task<TModel> DetailsAsync<TModel>(int id);

        Task<Course> FindAsync(int id);
    }
}
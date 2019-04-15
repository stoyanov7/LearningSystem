namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Html.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;
    using Utilities.Common;
    using Utilities.Constants;

    /// <summary>
    /// Services to entering course data.
    /// </summary>
    public class AdminCoursesService : IAdminCoursesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<LearningSystemContext, Course> context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminCoursesService"/> class.
        /// </summary>
        public AdminCoursesService(IMapper mapper, IRepository<LearningSystemContext, Course> context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        /// <summary>
        /// Add new course to the database.
        /// </summary>
        /// <typeparam name="TModel">Course model.</typeparam>
        /// <param name="model">Course for adding.</param>
        /// <returns>Return the mapped course.</returns>
        public async Task<Course> AddCourseAsync<TModel>(TModel model)
        {
            CoreValidator.EnsureNotNull(model, AdminConstants.NullCourse);
            var course = this.mapper.Map<Course>(model);

            await this.context.AddAsync(course);

            return course;
        }

        /// <summary>
        /// Get all courses.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns>List with all courses.</returns>
        public async Task<IEnumerable<TModel>> GetCoursesAsync<TModel>()
        {
            var courses = await this.context
                .Details()
                .ToListAsync();

            var model = this.mapper.Map<IEnumerable<TModel>>(courses);

            return model;
        }

        /// <summary>
        /// Get details for course by given key.
        /// </summary>
        /// <typeparam name="TModel">Model of course.</typeparam>
        /// <param name="id">Course key.</param>
        /// <returns>Mapped information for the course.</returns>
        public async Task<TModel> CourseDetailsAsync<TModel>(int id)
        {
            var model = await this.context
                .Details()
                .Include(c => c.Instances)
                .FirstOrDefaultAsync(x => x.Id == id);

            return this.mapper.Map<TModel>(model);
        }

        /// <summary>
        /// Find course by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Course> FindCourseAsync(int id) => await this.context.FindByIdAsync(id);
    }
}
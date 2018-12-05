namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;
    using Utilities.Common;

    /// <summary>
    /// Services to entering course data.
    /// </summary>
    public class CoursesService : ICoursesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Course> context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoursesService"/> class.
        /// </summary>
        public CoursesService(IMapper mapper, IRepository<Course> context)
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
            CoreValidator.EnsureNotNull(model, "The course is null");
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
        public async Task<TModel> DetailsAsync<TModel>(int id)
        {
            var model = await this.context
                .Details()
                .Include(c => c.Instances)
                .FirstOrDefaultAsync(x => x.Id == id);

            return this.mapper.Map<TModel>(model);
        }

        public async Task<Course> FindAsync(int id) => await this.context.FindByIdAsync(id);
    }
}
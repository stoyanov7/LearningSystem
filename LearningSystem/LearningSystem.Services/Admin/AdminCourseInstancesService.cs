namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;

    public class AdminCourseInstancesService : IAdminCourseInstancesService
    {
        private readonly IRepository<LearningSystemContext, CourseInstance> repository;
        private readonly IMapper mapper;

        public AdminCourseInstancesService(IRepository<LearningSystemContext, CourseInstance> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Create a new course instances
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateCourseIntancesAsync<TModel>(TModel model)
        {
            var instance = this.mapper.Map<CourseInstance>(model);
            await this.repository.AddAsync(instance);

            return instance.Id;
        }

        /// <summary>
        /// Get details for course instances
        /// </summary>
        /// <typeparam name="TModel">Course instance model</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TModel> CourseInstancesDetailsAsync<TModel>(int id)
        {
            var courseInstances = await this.repository
                .Details()
                .Include(c => c.Lectures)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            var model = this.mapper.Map<TModel>(courseInstances);
            return model;
        }

        /// <summary>
        /// List with all course instances.
        /// </summary>
        /// <typeparam name="TModel">Course instance model.</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> AllCourseInstancesAsync<TModel>()
        {
            var courseInstances = await this.repository
                .Get()
                .AsQueryable()
                .ToListAsync();

            var model = this.mapper.Map<IEnumerable<TModel>>(courseInstances);
            return model;
        }
    }
}
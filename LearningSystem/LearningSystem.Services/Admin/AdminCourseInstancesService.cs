namespace LearningSystem.Services.Admin
{
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

        public async Task<int> CreateCourseIntancesAsync<TModel>(TModel model)
        {
            var instance = this.mapper.Map<CourseInstance>(model);
            await this.repository.AddAsync(instance);

            return instance.Id;
        }

        public async Task<TModel> CourseInstancesDetailsAsync<TModel>(int id)
        {
            var model = await this.repository
                .Details()
                .Include(c => c.Lectures)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            return this.mapper.Map<TModel>(model);
        }
    }
}
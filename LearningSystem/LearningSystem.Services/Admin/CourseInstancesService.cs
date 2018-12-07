namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;

    public class CourseInstancesService : ICourseInstancesService
    {
        private readonly IRepository<CourseInstance> repository;
        private readonly IMapper mapper;

        public CourseInstancesService(IRepository<CourseInstance> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Create<TModel>(TModel model)
        {
            var instance = this.mapper.Map<CourseInstance>(model);
            await this.repository.AddAsync(instance);

            return instance.Id;
        }

        public async Task<TModel> DetailsAsync<TModel>(int id)
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
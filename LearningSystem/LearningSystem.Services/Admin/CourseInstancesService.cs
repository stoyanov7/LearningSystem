namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
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

        public async Task Create<TModel>(TModel model)
        {
            var instance = this.mapper.Map<CourseInstance>(model);
            await this.repository.AddAsync(instance);
        }
    }
}
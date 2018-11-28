namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Models;
    using ViewModels.Admin;

    public class CoursesService : ICoursesService
    {
        private readonly IMapper mapper;
        private readonly LearningSystemContext context;

        public CoursesService(IMapper mapper, LearningSystemContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task AddCourseAsync(CreateCourseBindingModel model)
        {
            var course = this.mapper.Map<Course>(model);

            await this.context.Courses.AddAsync(course);
            await this.context.SaveChangesAsync();
        }
    }
}
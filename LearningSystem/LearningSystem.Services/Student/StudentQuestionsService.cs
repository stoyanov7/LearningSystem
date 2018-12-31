namespace LearningSystem.Services.Student
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;

    public class StudentQuestionsService : IStudentQuestionsService
    {
        private readonly IRepository<LearningSystemContext, CourseInstance> repository;
        private readonly IMapper mapper;

        public StudentQuestionsService(IRepository<LearningSystemContext, CourseInstance> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<TModel> GetCourseInstanceAsync<TModel>(string questionSlug)
        {
            var course = await this.repository
                .Details()
                .FirstOrDefaultAsync(x => x.Slug == questionSlug);

            if (course == null)
            {
                return default(TModel);
            }

            var model = this.mapper.Map<TModel>(course);

            return model;
        }
    }
}
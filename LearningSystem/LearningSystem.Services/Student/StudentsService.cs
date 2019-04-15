namespace LearningSystem.Services.Student
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Identity;
    using Repository.Contracts;

    public class StudentsService : IStudentsService
    {
        private readonly IRepository<LearningSystemContext, ApplicationUser> repository;
        private readonly IRepository<LearningSystemContext, StudentsInCourses> studentsInCoursesRepository;
        private readonly IMapper mapper;

        public StudentsService(IRepository<LearningSystemContext, ApplicationUser> repository,
            IMapper mapper,
            IRepository<LearningSystemContext, StudentsInCourses> studentsInCoursesRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.studentsInCoursesRepository = studentsInCoursesRepository;
        }

        public async Task<IEnumerable<TModel>> FindAsync<TModel>(string searchText)
        {
            searchText = searchText ?? string.Empty;

            var user =  await this.repository
                .Get()
                .AsQueryable()
                .OrderBy(u => u.UserName)
                .Where(u => u.UserName.ToLower().Contains(searchText.ToLower()))
                .ToListAsync();

            var model = this.mapper.Map<IEnumerable<TModel>>(user);

            return model;
        }

        public async Task<IEnumerable<TModel>> EnrolledCourses<TModel>(string studentId)
        {
            var enrolledCourses = await this.studentsInCoursesRepository
                .Get()
                .AsQueryable()
                .Include(x => x.Course)
                .Where(x => x.StudentId == studentId)
                .ToListAsync();

            var model = this.mapper.Map<IEnumerable<TModel>>(enrolledCourses);

            return model;
        }

        public async Task<bool> IsUserEnrolledInCourse(string studentId, int courseId)
        {
            var enrolledCourses = await this.studentsInCoursesRepository
                .Details()
                .FirstOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId);

            return enrolledCourses != null;
        }
    }
}
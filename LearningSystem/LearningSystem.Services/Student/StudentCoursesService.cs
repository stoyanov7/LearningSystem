namespace LearningSystem.Services.Student
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;

    public class StudentCoursesService : IStudentCoursesService
    {
        private readonly IRepository<LearningSystemContext, Course> repository;
        private readonly IMapper mapper;

        public StudentCoursesService(IRepository<LearningSystemContext, Course> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IEnumerable<SelectListItem> GetCoursesForDropdownList()
        {
            return this.repository
                .Get()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        public async Task<TModel> GetCourseAsync<TModel>(int courseId)
        {
            var course = await this.repository
                .Details()
                .Include(x => x.Instances)
                .SingleOrDefaultAsync(x => x.Id == courseId);

            var model = this.mapper.Map<TModel>(course);

            return model;
        }

        public async Task<TModel> GetAllCoursesAsync<TModel>()
        {
            var courses = await this.repository
                .Details()
                .Include(x => x.Instances)
                .ToListAsync();

            var model = this.mapper.Map<TModel>(courses);

            return model;
        }
    }
}
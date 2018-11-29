namespace LearningSystem.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CoursesService : ICoursesService
    {
        private readonly IMapper mapper;
        private readonly LearningSystemContext context;

        public CoursesService(IMapper mapper, LearningSystemContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task AddCourseAsync<TModel>(TModel model)
        {
            var course = this.mapper.Map<Course>(model);

            await this.context.Courses.AddAsync(course);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<TModel> All<TModel>() => this.By<TModel>().AsEnumerable();

        public async Task<TModel> Details<TModel>(int id)
        { 
            var model = await this.context
                .Courses
                .Include(c => c.Instances)
                .FirstOrDefaultAsync(x => x.Id == id);

            return this.mapper.Map<TModel>(model);
        }

        private IQueryable<TModel> By<TModel>(Expression<Func<Course, bool>> predicate = null)
            => this.context
                .Courses
                .AsQueryable()
                .Where(predicate ?? (i => true))
                .ProjectTo<TModel>();
    }
}
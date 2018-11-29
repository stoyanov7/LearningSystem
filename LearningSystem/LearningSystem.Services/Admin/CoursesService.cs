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
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Repository.Contracts;

    public class CoursesService : ICoursesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Course> context;

        public CoursesService(IMapper mapper, IRepository<Course> context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<Course> AddCourseAsync<TModel>(TModel model)
        {
            var course = this.mapper.Map<Course>(model);
            await this.context.AddAsync(course);

            return course;
        }

        public IEnumerable<TModel> All<TModel>() => this.By<TModel>().AsEnumerable();

        public async Task<TModel> DetailsAsync<TModel>(int id)
        {
            var model = await this.context
                .Details()
                .Include(c => c.Instances)
                .FirstOrDefaultAsync(x => x.Id == id);

            return this.mapper.Map<TModel>(model);
        }

        private IQueryable<TModel> By<TModel>(Expression<Func<Course, bool>> predicate = null)
            => this.context
                .Get()
                .AsQueryable()
                .Where(predicate ?? (i => true))
                .ProjectTo<TModel>();
    }
}
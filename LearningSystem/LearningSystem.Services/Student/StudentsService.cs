namespace LearningSystem.Services.Student
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Identity;
    using Repository.Contracts;

    public class StudentsService : IStudentsService
    {
        private readonly IRepository<LearningSystemContext, ApplicationUser> repository;
        private readonly IMapper mapper;

        public StudentsService(IRepository<LearningSystemContext, ApplicationUser> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
    }
}
namespace LearningSystem.Services.Lecturer
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Identity;
    using Repository.Contracts;

    public class LecturerCourseInstancesService : ILecturerCourseInstancesService
    {
        private readonly IRepository<CourseInstance> repository;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public LecturerCourseInstancesService(IRepository<CourseInstance> repository, 
            IMapper mapper, 
            UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<TModel> DetailsAsync<TModel>(int id)
        {
            var model = await this.repository.Details()
                .Include(c => c.Lectures)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            return this.mapper.Map<TModel>(model);
        }

        public async Task<TModel> PrepareInstanceForEditingAsync<TModel>(int id)
        {
            var instance = await this.repository.Details()
                .Include(l => l.Lectures)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var instanceModel = this.mapper.Map<TModel>(instance);

            return instanceModel;
        }

        public async Task EditAsync(int id, ClaimsPrincipal user, string name, string description, DateTime startDate, DateTime endDate)
        {
            var courseInstance = await this.repository.FindByIdAsync(id);
            var isLecturer = courseInstance.LecturerId == this.userManager.GetUserId(user);
            var isAdmin = user.IsInRole("Administrator");

            if (!isLecturer && !isAdmin)
            {
                throw new InvalidOperationException();
            }

            courseInstance.Name = name;
            courseInstance.Description = description;
            courseInstance.StartDate = startDate;
            courseInstance.EndDate = endDate;

            await this.repository.SaveChangesAsync();
        }
    }
}
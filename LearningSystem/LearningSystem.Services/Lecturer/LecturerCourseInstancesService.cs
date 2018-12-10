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

        public async Task<TModel> CourseInstanceDetailsAsync<TModel>(int id)
        {
            var courseInstance = await this.repository.Details()
                .Include(c => c.Lectures)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            var model = this.mapper.Map<TModel>(courseInstance);
            return model;
        }

        public async Task<TModel> PrepareCourseInstanceForEditingAsync<TModel>(int id)
        {
            var instance = await this.repository
                .Details()
                .Include(l => l.Lectures)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instance == null)
            {
                throw new NullReferenceException();
            }

            var instanceModel = this.mapper.Map<TModel>(instance);

            return instanceModel;
        }

        public async Task EditCourseInstanceAsync(int id, ClaimsPrincipal user, string name, string description, DateTime startDate, DateTime endDate)
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

        public async Task<bool> IsCourseInstanceExist(int id) 
            => await this.repository
                   .FindByIdAsync(id) != null;

        public async Task AddLecture<TModel>(int id, TModel model)
        {
            var courseInstance = await this.repository.FindByIdAsync(id);
            var lecture = this.mapper.Map<Lecture>(model);
            courseInstance.Lectures.Add(lecture);

            await this.repository.SaveChangesAsync();
        }
    }
}
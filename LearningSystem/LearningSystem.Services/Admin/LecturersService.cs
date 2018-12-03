namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using Models.Identity;

    public class LecturersService : ILecturersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public LecturersService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TModel>> GetAllLecturers<TModel>()
        {
            var users = await this.userManager.GetUsersInRoleAsync("Lecturer"); 
            var lecturers = this.mapper.Map<IEnumerable<TModel>>(users);

            return lecturers;
        }
    }
}
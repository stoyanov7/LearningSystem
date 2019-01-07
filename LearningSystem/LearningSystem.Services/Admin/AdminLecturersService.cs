namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Microsoft.AspNetCore.Identity;
    using Models.Identity;
    using Utilities.Constants;

    public class AdminLecturersService : IAdminLecturersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AdminLecturersService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        /// <summary>
        /// Return list with all lectures.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> GetAllLecturersAsync<TModel>()
        {
            var users = await this.userManager.GetUsersInRoleAsync(AdminConstants.Lecturer); 
            var lecturers = this.mapper.Map<IEnumerable<TModel>>(users);

            return lecturers;
        }
    }
}
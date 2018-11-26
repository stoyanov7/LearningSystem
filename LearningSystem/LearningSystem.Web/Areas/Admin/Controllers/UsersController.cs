namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using LearningSystem.Models.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels;
    using Services.Contracts;

    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UsersController(IUsersService usersService, 
            UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.usersService
                .All<AllUsersViewModel>()
                .ToListAsync();

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await this.usersService.Find(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            var model = this.mapper.Map<UserDetailsViewModel>(user);
            model.Roles = roles;

            return this.View(model);
        }
    }
}
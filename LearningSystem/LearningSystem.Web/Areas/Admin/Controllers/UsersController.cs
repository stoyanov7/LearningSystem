namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using LearningSystem.Models.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Contracts;

    /// <summary>
    /// Controller for viewing users.
    /// </summary>
    public class UsersController : AdminController
    {
        private readonly IAdminUsersService adminUsersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController(IAdminUsersService adminUsersService, 
            UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            this.adminUsersService = adminUsersService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        /// <summary>
        /// Display list of all registered users.
        /// </summary>
        /// <returns>List with all users.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await this.adminUsersService
                .AllUsersAsync<AllUsersViewModel>(this.User);

            return this.View(model);
        }

        /// <summary>
        /// Details for user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>View with user information.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var currentUser = await this.userManager
                .GetUserAsync(this.User);

            if (id == currentUser.Id)
            {
                return this.Unauthorized();
            }

            var user = await this.adminUsersService.FindUserAsync(id);

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
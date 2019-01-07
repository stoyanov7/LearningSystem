namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using LearningSystem.Models.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.User;
    using Services.Admin.Contracts;

    /// <summary>
    /// Controller for viewing users.
    /// </summary>
    public class UsersController : AdminController
    {
        private const string InvalidIdentityDetails = "Invalid identity details.";

        private readonly IAdminUsersService adminUsersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController(IAdminUsersService adminUsersService, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            this.adminUsersService = adminUsersService;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            foreach (var allUsersViewModel in model)
            {
                allUsersViewModel.Roles = roles;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleBindingModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, InvalidIdentityDetails);
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            //TempData.AddSuccessMessage($"User {user.UserName} successfully added to the {model.Role} role.");
            return this.RedirectToAction(nameof(this.Index));
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
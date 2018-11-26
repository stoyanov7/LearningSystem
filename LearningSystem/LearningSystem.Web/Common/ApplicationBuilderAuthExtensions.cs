namespace LearningSystem.Web.Common
{
    using Data;
    using LearningSystem.Models.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Lecturer")
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>();

            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope
                    .ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = await userManager.FindByNameAsync("admin");

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com"
                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }
            }
        }
    }
}
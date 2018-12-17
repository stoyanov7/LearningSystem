﻿namespace LearningSystem.Utilities.Common
{
    using Models.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "123";
        private const string DetaultLecturerPassword = "123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole("Administrator"),
            new IdentityRole("Lecturer"),
            new IdentityRole("Blogger")
        };

        /// <summary>
        /// Middleware for seed data into database.
        /// </summary>
        /// <param name="app"></param>
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
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        FullName = "Admin Adminov"
                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }

                var lecturer = await userManager.FindByNameAsync("lecturer");
                if (lecturer == null)
                {
                    lecturer = new ApplicationUser
                    {
                        UserName = "lecturer",
                        Email = "lecturer@gmail.com"
                    };

                    await userManager.CreateAsync(lecturer, DetaultLecturerPassword);
                    await userManager.AddToRoleAsync(lecturer, roles[1].Name);
                }
            }
        }
    }
}
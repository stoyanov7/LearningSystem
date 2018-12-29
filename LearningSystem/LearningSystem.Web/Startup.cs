namespace LearningSystem.Web
{
    using AutoMapper;
    using Data;
    using Hubs;
    using LearningSystem.Models.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    using Repository.Contracts;
    using Services.Admin;
    using Services.Admin.Contracts;
    using Services.Blog;
    using Services.Blog.Contracts;
    using Services.Html;
    using Services.Html.Contracts;
    using Services.Identity;
    using Services.Lecturer;
    using Services.Lecturer.Contracts;
    using Services.Student;
    using Services.Student.Contracts;
    using Utilities.Common;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en");
                options.AddSupportedCultures("en", "bg");
                options.AddSupportedUICultures("en", "bg");
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            this.RegisterDatabases(services);

            this.RegisterIdentityAndAuthentication(services);
             
            this.RegisterServices(services);

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddAutoMapper(cfg => cfg.ValidateInlineMaps = false);
            services.AddSession();

            services.AddSignalR();

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.SeedDatabase();

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(options =>
            {
                options
                    .WithOrigins("https:localhost:44319")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();
            app.UseSignalR(options => options.MapHub<QuestionHub>("/questions"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterDatabases(IServiceCollection services)
        {
            services.AddDbContext<LearningSystemContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<LearningSystemPaymentsContext>(options =>
                options.UseMySql(this.Configuration.GetConnectionString("DefaultConnectionPayments")));
        }

        private void RegisterIdentityAndAuthentication(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.User.RequireUniqueEmail = true;
            })
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<LearningSystemContext>();

            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = this.Configuration["Authentication:Microsoft:ApplicationId"];
                    microsoftOptions.ClientSecret = this.Configuration["Authentication:Microsoft:Password"];
                });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAdminUsersService, AdminUsersService>();
            services.AddTransient<IAdminCoursesService, AdminCoursesService>();
            services.AddTransient<IAdminCourseInstancesService, AdminCourseInstancesService>();
            services.AddTransient<IAdminLecturersService, AdminLecturersService>();

            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<IStudentCoursesService, StudentCoursesService>();
            services.AddTransient<IStudentPaymentsService, StudentPaymentsService>();
            services.AddTransient<IStudentCourseInstancesService, StudentCourseInstancesService>();

            services.AddTransient<ILecturerCourseInstancesService, LecturerCourseInstancesService>();

            services.AddTransient<IBlogArticleService, BlogArticleService>();
            services.AddTransient<IHtmlService, HtmlService>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}

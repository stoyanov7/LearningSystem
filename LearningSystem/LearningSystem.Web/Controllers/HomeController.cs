namespace LearningSystem.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Search;
    using Services.Student.Contracts;

    public class HomeController : Controller
    {
        private readonly IStudentCourseInstancesService studentCourseInstancesService;
        private readonly IStudentsService studentsService;

        public HomeController(IStudentCourseInstancesService studentCourseInstancesService, 
            IStudentsService studentsService)
        {
            this.studentCourseInstancesService = studentCourseInstancesService;
            this.studentsService = studentsService;
        }

        public IActionResult Index() => this.View();

        public async Task<IActionResult> Search(SearchFormBindingModel model)
        {
            var viewModel = new SearchViewModel { SearchText = model.SearchText };

            if (model.SearchInCourses)
            {
                viewModel.Courses = await this.studentCourseInstancesService
                    .GetCourseInstancesAsync<SearchCourseInstanceViewModel>(model.SearchText);
            }

            if (model.SearchInUsers)
            {
                viewModel.Users = await this.studentsService.FindAsync<SearchUsersViewModel>(model.SearchText);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            this.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return this.LocalRedirect(returnUrl);
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            };

            return this.View(model);
        }
    }
}

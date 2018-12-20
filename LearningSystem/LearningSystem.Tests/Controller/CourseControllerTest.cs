namespace LearningSystem.Tests.Controller
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Services.Admin.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Web.Areas.Admin.Controllers;
    using Moq;
    using LearningSystem.Utilities.Constants;
    using Web.Areas.Admin.Models;
    using Xunit;

    public class CourseControllerTest
    {
        private readonly Mock<IAdminCoursesService> courseServiceMock;

        public CourseControllerTest() 
        {
            this.courseServiceMock = new Mock<IAdminCoursesService>();
        } 

        [Fact]
        public void CorsesController_ShoudBeInAdminArea()
        {
            //Arrange & Act
            var area = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AreaAttribute) as AreaAttribute;

            // Assert:
            Assert.NotNull(area);
            Assert.Equal(AdminConstants.AdminArea, area.RouteValue);
        }

        [Fact]
        public void CorsesController_ShoudAuthorizeAdmins()
        {
            // Arrange & Act
            var authorization = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AuthorizeAttribute) as AuthorizeAttribute;

            //3. Assert:
            Assert.NotNull(authorization);
            Assert.Equal(AdminConstants.AdminRole, authorization.Roles);
        }

        [Fact]
        public void Index_WithValidModel_ShouldCallService()
        {
            // Arrange
            var isServiceCalled = false;

            this.courseServiceMock
                .Setup(x => x.GetCoursesAsync<AllCoursesViewModel>())
                .Callback(() => isServiceCalled = true);

            var controller = new CoursesController(this.courseServiceMock.Object, null, null);

            // Act
            var result = controller.Index();

            // Assert
            Assert.True(isServiceCalled);
        }

        [Fact]
        public async Task Index_ShouldReturnAllCourses()
        {
            // Arrange
            var model = new AllCoursesViewModel
            {
                Id = 1,
                Name = "First"
            };

            this.courseServiceMock
                .Setup(x => x.GetCoursesAsync<AllCoursesViewModel>())
                .ReturnsAsync(new[] {model});

            var controller = new CoursesController(this.courseServiceMock.Object, null, null);

            // Act
            var result = await controller.Index();

            // Assert
            var resultView = result as ViewResult;

            Assert.IsType<ViewResult>(result);
            Assert.NotNull(resultView?.Model);
            Assert.IsAssignableFrom<IEnumerable<AllCoursesViewModel>>(resultView.Model);
        }
        
        [Fact]
        public void Create_WithValidCourse_ShoudCallService()
        {
            // Arrange
            var model = new CreateCourseBindingModel();
            var serviceCalled = false;

            this.courseServiceMock
                .Setup(repo => repo.AddCourseAsync(model))
                .Callback(() => serviceCalled = true);

            var controller = new CoursesController(this.courseServiceMock.Object, null, null);

            // Act
            var result = controller.Create(model);

            // Assert
            Assert.True(serviceCalled);
        }
    }
}

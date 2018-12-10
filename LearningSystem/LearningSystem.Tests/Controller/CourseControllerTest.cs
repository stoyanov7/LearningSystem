namespace LearningSystem.Tests.Controller
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningSystem.Services.Admin.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Web.Areas.Admin.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Utilities.Constants;
    using Web.Areas.Admin.Models;

    [TestClass]
    public class CourseControllerTest
    {
        private Mock<IAdminCoursesService> courseServiceMock;

        [TestInitialize]
        public void TestInitialize() 
        {
            this.courseServiceMock = new Mock<IAdminCoursesService>();
        } 

        [TestMethod]
        public void CorsesController_ShoudBeInAdminArea()
        {
            //Arrange & Act
            var area = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AreaAttribute) as AreaAttribute;

            // Assert:
            Assert.IsNotNull(area);
            Assert.AreEqual(AdminConstants.AdminArea, area.RouteValue);
        }

        [TestMethod]
        public void CorsesController_ShoudAuthorizeAdmins()
        {
            // Arrange & Act
            var authorization = typeof(CoursesController)
                .GetCustomAttributes(true)
                .FirstOrDefault(atr => atr is AuthorizeAttribute) as AuthorizeAttribute;

            //3. Assert:
            Assert.IsNotNull(authorization);
            Assert.AreEqual(AdminConstants.AdminRole, authorization.Roles);
        }

        [TestMethod]
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
            Assert.IsTrue(isServiceCalled);
        }

        [TestMethod]
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

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(resultView?.Model);
            Assert.IsInstanceOfType(resultView.Model, typeof(IEnumerable<AllCoursesViewModel>));
        }
        
        [TestMethod]
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
            Assert.IsTrue(serviceCalled);
        }
    }
}

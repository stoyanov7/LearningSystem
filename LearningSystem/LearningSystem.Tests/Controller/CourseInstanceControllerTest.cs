namespace LearningSystem.Tests.Controller
{
    using LearningSystem.Services.Admin.Contracts;
    using Moq;
    using Web.Areas.Admin.Controllers;
    using Web.Areas.Admin.Models.Lecture;
    using Xunit;

    public class CourseInstanceControllerTest
    {
        private readonly Mock<IAdminCoursesService> adminCoursesServiceMock;
        private readonly Mock<IAdminLecturersService> adminLecturesServiceMock;

        public CourseInstanceControllerTest()
        {
            this.adminCoursesServiceMock = new Mock<IAdminCoursesService>();
            this.adminLecturesServiceMock = new Mock<IAdminLecturersService>();
        }

        [Fact]
        public void Create_WithValidModel_ShouldCallAdminCoursesService()
        {
            // Arrange
            var isServiceCalled = false;

            this.adminCoursesServiceMock
                .Setup(x => x.FindCourseAsync(It.IsAny<int>()))
                .Callback(() => isServiceCalled = true);

            var controller = new CourseInstancesController(null, this.adminCoursesServiceMock.Object, null, null);

            // Act
            var result = controller.Create(It.IsAny<int>());

            // Assert
            Assert.True(isServiceCalled);
        }
    }
}
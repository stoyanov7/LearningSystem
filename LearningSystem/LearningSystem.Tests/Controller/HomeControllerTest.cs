namespace LearningSystem.Tests.Controller
{
    using System.Threading.Tasks;
    using LearningSystem.Services.Admin.Contracts;
    using LearningSystem.Services.Blog.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Web.Areas.Admin.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public async Task Index_ReturnsTheProperView()
        {
            // Arrange
            var blogArticleServiceMock = new Mock<IBlogArticleService>();
            var adminCourseInstancesServiceMock = new Mock<IAdminCourseInstancesService>();
            var controller = new HomeController(blogArticleServiceMock.Object, adminCourseInstancesServiceMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
namespace LearningSystem.Tests.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using Web.Areas.Admin.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void Index_ReturnsTheProperView()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
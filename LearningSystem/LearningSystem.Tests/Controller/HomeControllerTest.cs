namespace LearningSystem.Tests.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Web.Areas.Admin.Controllers;

    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_ReturnsTheProperView()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
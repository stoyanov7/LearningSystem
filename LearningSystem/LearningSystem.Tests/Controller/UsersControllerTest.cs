namespace LearningSystem.Tests.Controller
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities.Constants;
    using Web.Areas.Admin.Controllers;

    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void Index_ShoudBeAccesseibleByAdmin()
        {
            var controller = new UsersController(null, null, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Role, AdminConstants.AdminRole)
                        }))
                    }
                }
            };

            Assert.IsTrue(controller.User.IsInRole(AdminConstants.AdminRole));
        }
    }
}
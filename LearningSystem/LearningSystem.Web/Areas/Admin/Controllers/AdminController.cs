namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Constants;

    [Area(AdminConstants.AdminArea)]
    [Authorize(Roles = AdminConstants.AdminRole)]
    public abstract class AdminController : Controller
    {
       
    }
}
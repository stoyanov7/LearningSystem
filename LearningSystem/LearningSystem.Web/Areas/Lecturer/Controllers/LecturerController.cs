namespace LearningSystem.Web.Areas.Lecturer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Constants;

    [Area(LecturerConstants.LecturerArea)]
    [Authorize(Roles = LecturerConstants.LecturerOrAdminRole)]
    public abstract class LecturerController : Controller
    {
    }
}
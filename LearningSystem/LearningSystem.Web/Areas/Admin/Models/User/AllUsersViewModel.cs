namespace LearningSystem.Web.Areas.Admin.Models.User
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AllUsersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
        
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
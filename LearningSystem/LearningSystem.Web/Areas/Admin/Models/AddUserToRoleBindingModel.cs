namespace LearningSystem.Web.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserToRoleBindingModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
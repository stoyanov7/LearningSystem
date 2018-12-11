namespace LearningSystem.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreatePaymentBindingModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Amount { get; set; }

        [Required]
        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}
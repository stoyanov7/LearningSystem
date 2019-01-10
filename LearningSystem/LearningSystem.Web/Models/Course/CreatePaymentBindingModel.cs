namespace LearningSystem.Web.Models.Course
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using LearningSystem.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Utilities.Infrastructure.Contracts;

    public class CreatePaymentBindingModel : IMapFrom<Payment>, IHaveCustomMapping
    {
        [Required]
        public string Username { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<CreatePaymentBindingModel, Payment>();
    }
}
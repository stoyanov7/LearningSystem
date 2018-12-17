namespace LearningSystem.Web.Areas.Admin.Models
{
    using System.Collections.Generic;
    using AutoMapper;
    using Infrastructure.Contracts;
    using LearningSystem.Models.Identity;

    public class UserDetailsViewModel : IMapFrom<ApplicationUser>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<ApplicationUser, UserDetailsViewModel>();
    }
}
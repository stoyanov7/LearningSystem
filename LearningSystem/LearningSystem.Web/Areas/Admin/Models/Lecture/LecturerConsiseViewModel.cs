namespace LearningSystem.Web.Areas.Admin.Models.Lecture
{
    using AutoMapper;
    using LearningSystem.Models.Identity;
    using Utilities.Infrastructure.Contracts;

    public class LecturerConsiseViewModel : IMapFrom<ApplicationUser>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<ApplicationUser, LecturerConsiseViewModel>();
        }
    }
}
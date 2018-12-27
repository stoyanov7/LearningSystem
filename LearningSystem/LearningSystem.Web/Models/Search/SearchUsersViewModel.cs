namespace LearningSystem.Web.Models.Search
{
    using AutoMapper;
    using LearningSystem.Models.Identity;
    using Utilities.Infrastructure.Contracts;

    public class SearchUsersViewModel : IMapFrom<ApplicationUser>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public int EnrolledCourses { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<ApplicationUser, SearchUsersViewModel>()
                .ForMember(u => u.Username, cfg => cfg.MapFrom(u => u.UserName))
                .ForMember(u => u.EnrolledCourses, cfg => cfg.MapFrom(u => u.EnrolledCourses.Count));
    }
}
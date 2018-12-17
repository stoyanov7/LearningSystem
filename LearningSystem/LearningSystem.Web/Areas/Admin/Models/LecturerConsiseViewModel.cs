namespace LearningSystem.Web.Areas.Admin.Models
{
    using AutoMapper;
    using Infrastructure.Contracts;
    using LearningSystem.Models.Identity;

    public class LecturerConsiseViewModel : IMapFrom<ApplicationUser>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<ApplicationUser, LecturerConsiseViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(u => u.UserName));
        }
    }
}
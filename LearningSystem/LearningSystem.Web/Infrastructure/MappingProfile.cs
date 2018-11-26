namespace LearningSystem.Web.Infrastructure
{
    using Areas.Admin.Models.ViewModels;
    using AutoMapper;
    using LearningSystem.Models.Identity;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ApplicationUser, AllUsersViewModel>();

            this.CreateMap<ApplicationUser, UserDetailsViewModel>();
        }
    }
}
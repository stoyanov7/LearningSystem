namespace LearningSystem.Web.Infrastructure
{
    using AutoMapper;
    using LearningSystem.Models.Identity;
    using ViewModels.Admin;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ApplicationUser, AllUsersViewModel>();

            this.CreateMap<ApplicationUser, UserDetailsViewModel>();
        }
    }
}
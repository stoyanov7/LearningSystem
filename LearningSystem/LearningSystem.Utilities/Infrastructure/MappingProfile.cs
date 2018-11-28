namespace LearningSystem.Utilities.Infrastructure
{
    using AutoMapper;
    using Models.Identity;
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
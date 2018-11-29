namespace LearningSystem.Web.Infrastructure
{
    using AutoMapper;
    using LearningSystem.Models;
    using LearningSystem.Models.Identity;
    using ViewModels.Admin;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ApplicationUser, AllUsersViewModel>();

            this.CreateMap<ApplicationUser, UserDetailsViewModel>();

            this.CreateMap<CreateCourseBindingModel, Course>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Instances, opt => opt.Ignore())
                .ForSourceMember(x => x.Id, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.Instances, opt => opt.DoNotValidate());

            this.CreateMap<Course, AllCoursesViewModel>();

            this.CreateMap<Course, CourseDetailsViewModel>();
        }
    }
}
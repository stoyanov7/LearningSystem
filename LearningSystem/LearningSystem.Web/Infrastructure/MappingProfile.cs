namespace LearningSystem.Web.Infrastructure
{
    using Areas.Admin.Models;
    using AutoMapper;
    using LearningSystem.Models;
    using LearningSystem.Models.Identity;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ApplicationUser, AllUsersViewModel>();

            this.CreateMap<ApplicationUser, UserDetailsViewModel>();

            this.CreateMap<CreateCourseBindingModel, Course>()
                .ForMember(x => x.Instances, opt => opt.Ignore())
                .ForSourceMember(x => x.Instances, opt => opt.DoNotValidate());

            this.CreateMap<Course, AllCoursesViewModel>();

            this.CreateMap<Course, CourseDetailsViewModel>();

            this.CreateMap<CreateCourseInstancesBindingModel, CourseInstance>();

            this.CreateMap<CreateCourseInstancesBindingModel, Course>();
        }
    }
}
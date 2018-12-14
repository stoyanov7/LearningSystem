namespace LearningSystem.Web.Infrastructure
{
    using Areas.Admin.Models;
    using Areas.Blog.Models;
    using Areas.Lecturer.Models;
    using AutoMapper;
    using LearningSystem.Models;
    using LearningSystem.Models.Identity;
    using Models;

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

            this.CreateMap<Course, StudentAllCoursesViewModel>();

            this.CreateMap<Course, CourseDetailsViewModel>();

            this.CreateMap<CreateCourseInstancesBindingModel, CourseInstance>();

            this.CreateMap<CreateCourseInstancesBindingModel, Course>();

            this.CreateMap<ApplicationUser, LecturerConsiseViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(u => u.UserName));

            this.CreateMap<Lecture, LectureShortViewModel>();

            this.CreateMap<CreateLectureBindingModel, Lecture>();

            this.CreateMap<CreatePaymentBindingModel, Payment>();

            this.CreateMap<PublishArticleBindingModel, Article>();
        }
    }
}
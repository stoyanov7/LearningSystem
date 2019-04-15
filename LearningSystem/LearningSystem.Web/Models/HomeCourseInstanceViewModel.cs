namespace LearningSystem.Web.Models
{
    using AutoMapper;
    using LearningSystem.Models;
    using Utilities.Infrastructure.Contracts;

    public class HomeCourseInstanceViewModel : IMapFrom<StudentsInCourses>, IHaveCustomMapping
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<HomeCourseInstanceViewModel, StudentsInCourses>();
            //.ForMember(x => x.Course.Id, opt => opt.MapFrom(x => x.CourseId))
            //.ForMember(x => x.Course.Name, opt => opt.MapFrom(x => x.CourseName))
            //.ForMember(x => x.Course.Description, opt => opt.MapFrom(x => x.CourseDescription));
        }
    }
}
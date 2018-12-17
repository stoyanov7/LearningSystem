namespace LearningSystem.Web.Areas.Lecturer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Infrastructure.Contracts;
    using LearningSystem.Models;

    public class CreateLectureBindingModel : IMapFrom<Lecture>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        [Display(Name = "Lecture Hall")]
        public string LectureHall { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public int CourseId { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<CreateLectureBindingModel, Lecture>();
    }
}
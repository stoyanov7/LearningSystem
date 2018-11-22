namespace LearningSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        [Required]
        public ResourceType Type { get; set; }

        [Required]
        public int Order { get; set; }

        [DataType(DataType.Url)]
        public string ResourceUrl { get; set; }
    }
}
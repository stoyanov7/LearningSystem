namespace LearningSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Identity;

    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
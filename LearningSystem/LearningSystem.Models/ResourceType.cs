namespace LearningSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ResourceType
    {
        public ResourceType() => this.Resources = new List<Resource>();

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
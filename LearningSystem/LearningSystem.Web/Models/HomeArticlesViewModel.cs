using System;

namespace LearningSystem.Web.Models
{
    public class HomeArticlesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }
    }
}
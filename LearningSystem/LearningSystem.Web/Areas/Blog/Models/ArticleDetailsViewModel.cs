namespace LearningSystem.Web.Areas.Blog.Models
{
    using System;

    public class ArticleDetailsViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }
    }
}
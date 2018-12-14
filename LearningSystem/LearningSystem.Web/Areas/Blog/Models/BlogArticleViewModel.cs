namespace LearningSystem.Web.Areas.Blog.Models
{
    using System;

    public class BlogArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }
    }
}
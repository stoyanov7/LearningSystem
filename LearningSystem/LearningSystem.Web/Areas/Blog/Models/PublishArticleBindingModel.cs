namespace LearningSystem.Web.Areas.Blog.Models
{
    using System;
    using AutoMapper;
    using LearningSystem.Models;
    using LearningSystem.Models.Identity;
    using Utilities.Infrastructure.Contracts;

    public class PublishArticleBindingModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper.CreateMap<PublishArticleBindingModel, Article>();
    }
}
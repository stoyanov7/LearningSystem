namespace LearningSystem.Web.Areas.Blog.Models
{
    using System;
    using AutoMapper;
    using Infrastructure.Contracts;
    using LearningSystem.Models;

    public class ArticleDetailsViewModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
        }
    }
}
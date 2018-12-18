namespace LearningSystem.Web.Areas.Blog.Models
{
    using AutoMapper;
    using LearningSystem.Models;
    using Utilities.Infrastructure.Contracts;

    public class PublishArticleBindingModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<PublishArticleBindingModel, Article>();
    }
}
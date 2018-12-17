namespace LearningSystem.Web.Areas.Blog.Models
{
    using AutoMapper;
    using Infrastructure.Contracts;
    using LearningSystem.Models;

    public class PublishArticleBindingModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public void ConfigureMapping(Profile mapper) 
            => mapper.CreateMap<PublishArticleBindingModel, Article>();
    }
}
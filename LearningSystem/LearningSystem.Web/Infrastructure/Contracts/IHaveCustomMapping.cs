namespace LearningSystem.Web.Infrastructure.Contracts
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
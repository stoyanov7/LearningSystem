namespace LearningSystem.Utilities.Infrastructure.Contracts
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
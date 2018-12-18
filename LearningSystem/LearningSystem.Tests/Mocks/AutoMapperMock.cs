namespace LearningSystem.Tests.Mocks
{
    using AutoMapper;
    using Utilities.Infrastructure;

    public class AutoMapperMock
    {
        static AutoMapperMock() 
            => Mapper.Initialize(config => config.AddProfile<MappingProfile>());

        public static IMapper GetMapper() => Mapper.Instance;
    }
}
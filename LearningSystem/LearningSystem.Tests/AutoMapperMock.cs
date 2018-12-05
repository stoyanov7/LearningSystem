namespace LearningSystem.Tests
{
    using AutoMapper;
    using Web.Infrastructure;

    public class AutoMapperMock
    {
        static AutoMapperMock()
        {
            Mapper.Initialize(config => config.AddProfile<MappingProfile>());
        }

        public static IMapper GetMapper() => Mapper.Instance;
    }
}
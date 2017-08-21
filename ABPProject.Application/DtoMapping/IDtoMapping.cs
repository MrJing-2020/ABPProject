using AutoMapper;

namespace ABPProject.DtoMapping
{
    internal interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}
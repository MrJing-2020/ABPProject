using ABPProject.Projects;
using ABPProject.Projects.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.DtoMapping
{
    public class ProjectMapping: IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            //定义单向映射
            //mapperConfig.CreateMap<EditProjectInput, Project>();

            //自定义映射
            //var taskDtoMapper = mapperConfig.CreateMap<EditProjectInput, Project>();
            //taskDtoMapper.ForMember(dto => dto.BeginDate, map => map.MapFrom(m => DateTime.ParseExact(m.BeginDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)));
        }
    }
}

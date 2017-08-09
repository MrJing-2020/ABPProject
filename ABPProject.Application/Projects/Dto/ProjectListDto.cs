using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Projects.Dto
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectListDto: EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

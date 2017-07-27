using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ABPProject.Authorization.Roles;
using System;

namespace ABPProject.Roles.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RoleListDto: EntityDto<int>
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public bool IsStatic { get; set; }

    }
}

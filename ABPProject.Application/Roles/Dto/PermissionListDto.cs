using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Roles.Dto
{
    [AutoMapFrom(typeof(Permission))]
    public class PermissionListDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}

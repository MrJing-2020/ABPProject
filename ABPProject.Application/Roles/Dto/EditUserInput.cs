using Abp.Authorization.Roles;
using Abp.AutoMapper;
using ABPProject.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class EditUserInput
    {
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        public bool IsStatic { get; set; }

    }
}

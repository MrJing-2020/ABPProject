using Abp.Authorization.Roles;
using Abp.AutoMapper;
using ABPProject.Authorization.Roles;
using System.ComponentModel.DataAnnotations;

namespace ABPProject.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class EditUserInput
    {
        public int? Id { get; set; }

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

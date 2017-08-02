using System.Threading.Tasks;
using Abp.Application.Services;
using ABPProject.Roles.Dto;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;

namespace ABPProject.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
        Task<ListResultDto<RoleListDto>> GetRoles();
        Task EditRole(EditUserInput input);
        PagedResultDto<RoleListDto> GetPagedRole(PageParams input);
        Task<EditUserInput> GetRoleById(OneParam param);
    }
}

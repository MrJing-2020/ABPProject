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
        Task CreateRole(EditUserInput input);
        PagedResultDto<RoleListDto> GetPagedRole(PageParams input);
    }
}

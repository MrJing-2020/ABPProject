using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.Users.Dto;
using ABPProject.CommonDto;

namespace ABPProject.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);
        Task RemoveFromRole(long userId, string roleName);
        Task<ListResultDto<UserListDto>> GetUsers();
        Task CreateUser(CreateUserInput input);
        Task AddToRoles(long userId, params string[] roles);
        Task AddToRole(long userId, string roleName);
        PagedResultDto<UserListDto> GetPagedUser(PageParams pageArg);
        Task DeleteUser(ArrayParams param);
    }
}
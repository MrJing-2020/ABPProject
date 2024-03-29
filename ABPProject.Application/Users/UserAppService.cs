using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using ABPProject.Authorization;
using ABPProject.Users.Dto;
using Microsoft.AspNet.Identity;
using ABPProject.CommonDto;
using System.Linq;
using ABPProject.Extend;
using Abp.Linq.Extensions;

namespace ABPProject.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.User)]
    public class UserAppService : ABPProjectAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
        }

        /// <summary>
        /// 禁用用户权限（优先级高于角色权限）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);
            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GrantPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);
            await UserManager.GrantPermissionAsync(user, permission);
        }


        /// <summary>
        /// 添加用户到多个角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task AddToRoles(KeyValuesParam<long, string> param)
        {
            CheckErrors(await UserManager.AddToRolesAsync(param.Key, param.Values));
        }

        //public async Task<IdentityResult> AddToRoles(long userId, params string[] roles)
        //{
        //    var user = await UserManager.GetUserByIdAsync(userId);
        //    var result = await UserManager.SetRoles(user, roles);
        //    return result;
        //}

        /// <summary>
        /// 把用户从角色中移除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task RemoveFromRole(dynamic param)
        {
            var userId = (long)param.UserId;
            var roleName = param.RoleName.ToString();
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        //[AbpAuthorize(PermissionNames.Pages)]
        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();
            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;
            //user.Surname = "#";

            CheckErrors(await UserManager.CreateAsync(user));
        }

        /// <summary>
        /// 将用户加入到组织单元（组织单元用于控制数据权限）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        public async Task AddToOrganizationUnit(long userId, long ouId)
        {
            await UserManager.AddToOrganizationUnitAsync(userId,ouId);
        }

        /// <summary>
        /// 角色分页控制器服务/API接口
        /// </summary>
        /// <param name="pageArg"></param>
        /// <returns></returns>
        public PagedResultDto<UserListDto> GetPagedUser(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<User> users = null;
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                users = _userRepository.GetAll().Where(m => m.FullName.Contains(input.SearchText) || m.UserName.Contains(input.SearchText)||m.EmailAddress.Contains(input.SearchText));
                count = users.Count();
            }
            else
            {
                users = _userRepository.GetAll();
                count = _userRepository.Count();
            }
            if (!string.IsNullOrEmpty(input.SortName))
            {
                //将首字母转成大写
                input.SortName = input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1);
                users = input.SortOrder == "desc" ? users.OrderBy(input.SortName, true).PageBy(input.PageInput) :
                users.OrderBy(input.SortName).PageBy(input.PageInput);
            }
            else
            {
                //默认按时间降序
                users = users.OrderByDescending(m => m.CreationTime).PageBy(input.PageInput);
            }
            return new PagedResultDto<UserListDto>(count, users.MapTo<List<UserListDto>>());
        }

        /// <summary>
        /// 用户批量删除
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task DeleteUser(ArrayParams param)
        {
            await _userRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
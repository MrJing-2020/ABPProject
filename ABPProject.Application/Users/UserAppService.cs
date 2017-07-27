using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using ABPProject.Authorization;
using ABPProject.Users.Dto;
using Microsoft.AspNet.Identity;

namespace ABPProject.Users
{
    /* THIS IS JUST A SAMPLE. */
    //[AbpAuthorize(PermissionNames.Pages_Users)]
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
        /// 添加用户到角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task AddToRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.AddToRoleAsync(userId, roleName));
        }

        /// <summary>
        /// 添加用户到多个角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task AddToRoles(long userId, params string[] roles)
        {
            CheckErrors(await UserManager.AddToRolesAsync(userId, roles));
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
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        [AbpAuthorize(PermissionNames.Pages)]
        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

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
    }
}
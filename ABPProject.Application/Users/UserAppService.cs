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
        /// �����û�Ȩ�ޣ����ȼ����ڽ�ɫȨ�ޣ�
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
        /// �û���Ȩ
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
        /// ����û�����ɫ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task AddToRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.AddToRoleAsync(userId, roleName));
        }

        /// <summary>
        /// ����û��������ɫ
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
        /// ���û��ӽ�ɫ���Ƴ�
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
        /// ���û����뵽��֯��Ԫ����֯��Ԫ���ڿ�������Ȩ�ޣ�
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
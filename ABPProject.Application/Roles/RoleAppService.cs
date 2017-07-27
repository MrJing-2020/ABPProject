using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using ABPProject.Authorization.Roles;
using ABPProject.Roles.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using Abp.Linq.Extensions;

namespace ABPProject.Roles
{
    public class RoleAppService : ABPProjectAppServiceBase,IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IRepository<Role, int> _roleRepository;

        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager, IRepository<Role, int> roleRepository)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _roleRepository = roleRepository;
        }

        public async Task<ListResultDto<RoleListDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();

            return new ListResultDto<RoleListDto>(
                roles.MapTo<List<RoleListDto>>()
                );
        }

        //权限设置
        [AbpAuthorize(PermissionNames.Pages)]
        public async Task CreateRole(EditUserInput input)
        {
            var role = input.MapTo<Role>();
            role.TenantId = AbpSession.TenantId;
            CheckErrors(await _roleManager.CreateAsync(role));
        }

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        public PagedResultDto<RoleListDto> GetPagedRole(IPagedResultRequest input)
        {
            var count = _roleRepository.Count();
            var roles = _roleRepository.GetAll().PageBy(input);
            return new PagedResultDto<RoleListDto>(count, roles.MapTo<List<RoleListDto>>());
        }
    }
}
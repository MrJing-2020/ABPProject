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
using ABPProject.Extend;

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

        /// <summary>
        /// 根据id获取角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public async Task<EditUserInput> GetRoleById(OneParam param)
        {
            var roles = await _roleRepository.GetAsync(param.id);
            return roles.MapTo<EditUserInput>();
        }

        //权限设置
        [AbpAuthorize(PermissionNames.Pages)]
        public async Task EditRole(EditUserInput input)
        {
            var role = input.MapTo<Role>();
            role.TenantId = AbpSession.TenantId;
            await _roleRepository.InsertOrUpdateAsync(role);
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

        /// <summary>
        /// 角色分页控制器服务/API接口
        /// </summary>
        /// <param name="pageArg"></param>
        /// <returns></returns>
        public PagedResultDto<RoleListDto> GetPagedRole(PageParams pageArg )
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<Role> roles = null;
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                roles = _roleRepository.GetAll().Where(m => m.Name.Contains(input.SearchText) || m.DisplayName.Contains(input.SearchText));
                count = roles.Count();
            }
            else
            {
                roles = _roleRepository.GetAll();
                count = _roleRepository.Count();
            }
            if (!string.IsNullOrEmpty(input.SortName))
            {
                //将首字母转成大写
                input.SortName = input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1);
                roles = input.SortOrder == "desc" ? roles.OrderBy(input.SortName,true).PageBy(input.PageInput) :
                roles.OrderBy(input.SortName).PageBy(input.PageInput);
            }
            else
            {
                //默认按时间降序
                roles= roles.OrderByDescending(m=>m.CreationTime).PageBy(input.PageInput);
            }
            return new PagedResultDto<RoleListDto>(count, roles.MapTo<List<RoleListDto>>());
        }
    }
}
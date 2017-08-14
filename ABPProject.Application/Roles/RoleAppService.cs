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
    [AbpAuthorize(PermissionNames.Role)]
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

        public async Task<object> GetRolesByUser(OneParam param)
        {
            var roles = await _roleRepository.GetAllListAsync();
            var inRoles = await UserManager.GetRolesAsync(param.Id);
            return new { allRoles = roles.MapTo<List<RoleListDto>>(), inRoles = inRoles.ToArray() };
        }

        /// <summary>
        /// 根据id获取角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public async Task<EditUserInput> GetRoleById(OneParam param)
        {
            var roles = await _roleRepository.GetAsync(param.Id);
            return roles.MapTo<EditUserInput>();
        }

        //权限设置
        [AbpAuthorize(PermissionNames.Role_Edit)]
        public async Task EditRole(EditUserInput input)
        {
            var id = input.Id;
            var role = input.MapTo<Role>();
            role.TenantId = AbpSession.TenantId;
            if (id == null)
            {
                CheckErrors(await _roleManager.CreateAsync(role));
            }
            else
            {
                var isRepetition = await _roleManager.CheckDuplicateRoleNameAsync(id, input.Name, input.DisplayName);
                CheckErrors(isRepetition);
                if (isRepetition.Succeeded == true)
                {
                    var roleUpdate = await _roleManager.GetRoleByIdAsync(Convert.ToInt32(id));
                    roleUpdate.Name = input.Name;
                    roleUpdate.DisplayName = input.DisplayName;
                    roleUpdate.IsStatic = input.IsStatic;
                    await _roleManager.UpdateAsync(roleUpdate);
                }
            }
            //var role = input.MapTo<Role>();
            //await _roleRepository.InsertOrUpdateAsync(role);
        }

        public async Task<object> GetPermissionsByRole(OneParam param)
        {
            var rolePermissions = await _roleManager.GetGrantedPermissionsAsync(param.Id);
            var allPermissons = _permissionManager.GetAllPermissions().ToList();
            var permissionGroup = new List<PermissionGroupDto>();
            foreach (var item in allPermissons.Where(m=>m.Children.Count()>0))
            {
                PermissionGroupDto tempData = item.MapTo<PermissionGroupDto>();
                tempData.PermissionGroups = item.Children.ToList().MapTo<List<PermissionListDto>>();
                permissionGroup.Add(tempData);
            }
            return new { allPermissons = permissionGroup, rolePermissions = rolePermissions.Select(m=>m.Name).ToList() };
            //return new { allPermissons = allPermissons.MapTo<List<PermissionListDto>>(), rolePermissions = rolePermissions.Select(m=>m.Name).ToList() };
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

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="param">例：{ids: [14, 13]}</param>
        /// <returns></returns>
        public async Task DeleteRole(ArrayParams param)
        {
            await _roleRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Organizations;
using ABPProject.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.Projects
{
    public class ProjectManager : IDomainService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly UserManager _userManager;

        public ProjectManager(
            IRepository<Project> projectRepository, 
            IRepository<OrganizationUnit, long> organizationUnitRepository, 
            UserManager userManager)
        {
            _projectRepository = projectRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _userManager = userManager;
        }

        public async Task<List<Project>> GetProjecsInOu(long organizationUnitId)
        {
            return await _projectRepository.GetAllListAsync(m => m.OrganizationUnitId == organizationUnitId);
        }

        public async Task<List<Project>> GetProjecsForUserAsync(long userId)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            var organizationUnits = await _userManager.GetOrganizationUnitsAsync(user);
            var organizationUnitIds = organizationUnits.Select(ou => ou.Id);

            return await _projectRepository.GetAllListAsync(p => organizationUnitIds.Contains((long)p.OrganizationUnitId));
        }
    }
}

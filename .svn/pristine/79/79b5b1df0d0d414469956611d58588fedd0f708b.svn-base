using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using ABPProject.OrganizationUnits.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.OrganizationUnits
{
    public class OuAppService: ABPProjectAppServiceBase,IOuAppService
    {
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        public OuAppService(IRepository<OrganizationUnit, long> ouRepository)
        {
            _ouRepository = ouRepository;
        }

        public async Task<ListResultDto<OuListDto>> GetOrganizationUnits()
        {
            var organizationUnits = await _ouRepository.GetAllListAsync();
            return new ListResultDto<OuListDto>(organizationUnits.MapTo<List<OuListDto>>());
        }
    }
}

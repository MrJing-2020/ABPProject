using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Organizations;
using ABPProject.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders
{
    public class SalesOrderManager: IDomainService
    {
        private readonly IRepository<SalesOrder> _salesOrderRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly UserManager _userManager;

        public SalesOrderManager(
            IRepository<SalesOrder> salesOrderRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            UserManager userManager)
        {
            _salesOrderRepository = salesOrderRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _userManager = userManager;
        }

        public IQueryable<SalesOrder> GetProjecsInOu(long organizationUnitId)
        {
            return _salesOrderRepository.GetAllIncluding(m => m.SalesOrderItem);
        }

    }
}

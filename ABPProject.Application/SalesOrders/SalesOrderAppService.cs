using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ABPProject.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPProject.Extend;
using ABPProject.SalesOrders.Dto;
using Abp.Domain.Uow;
using ABPProject.Authorization;
using Abp.Authorization;

namespace ABPProject.SalesOrders
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class SalesOrderAppService: ABPProjectAppServiceBase, ISalesOrderAppService
    {
        private readonly SalesOrderManager _salesOrderManager;
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        private readonly IRepository<SalesOrderItem, int> _salesOrderItemRepository;

        public SalesOrderAppService(
            SalesOrderManager salesOrderManager, 
            IRepository<SalesOrder, int> salesOrderRepository,
            IRepository<SalesOrderItem, int> salesOrderItemRepository)
        {
            _salesOrderManager = salesOrderManager;
            _salesOrderRepository = salesOrderRepository;
            _salesOrderItemRepository = salesOrderItemRepository;
        }

        public PagedResultDto<SalesOrderListDto> GetPagedSalesOrder(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<SalesOrder> salesOrder = _salesOrderRepository.GetAllIncluding(m => m.SalesOrderItem);
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                salesOrder = salesOrder.Where(m => m.SalesName.Contains(input.SearchText) || m.SalesId.Contains(input.SearchText));
                count = salesOrder.Count();
            }
            else
            {
                count = _salesOrderRepository.Count();
            }
            if (!string.IsNullOrEmpty(input.SortName))
            {
                //将首字母转成大写
                input.SortName = input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1);
                salesOrder = input.SortOrder == "desc" ? salesOrder.OrderBy(input.SortName, true).PageBy(input.PageInput) :
                salesOrder.OrderBy(input.SortName).PageBy(input.PageInput);
            }
            else
            {
                //默认按时间降序
                salesOrder = salesOrder.OrderByDescending(m => m.CreationTime).PageBy(input.PageInput);
            }
            var resultList = salesOrder.ToList().MapTo<List<SalesOrderListDto>>();
            foreach (var item in resultList)
            {
                item.SalesOrderItems = salesOrder.Where(m => m.Id == item.Id).FirstOrDefault().SalesOrderItem.MapTo<List<SalesOrderItemListDto>>();
            }
            return new PagedResultDto<SalesOrderListDto>(count, resultList);
        }

        public EditSalesOrderInput GetSalesOrderById(OneParam param)
        {
            var salesOrder = _salesOrderRepository.GetAllIncluding(m=>m.SalesOrderItem).Where(m => m.Id == param.Id).FirstOrDefault();
            var salesOrderDto = salesOrder.MapTo<EditSalesOrderInput>();
            salesOrderDto.SalesOrderItems = salesOrder.SalesOrderItem.MapTo<List<EditSalesOrderItemDto>>();
            return salesOrderDto;
        }

        public async Task EditSalesOrder(EditSalesOrderInput input)
        {
           var salesOrder = input.MapTo<SalesOrder>();
           salesOrder.DeliveryDate = DateTime.Now;
           var salesOrderItems = input.SalesOrderItems.MapTo<List<SalesOrderItem>>();
           await _salesOrderRepository.InsertOrUpdateAsync(salesOrder);
            foreach (var item in salesOrderItems)
            {
                await _salesOrderItemRepository.InsertOrUpdateAsync(item);
            }
        }

        [UnitOfWork]
        public async Task DeleteSalesOrder(ArrayParams param)
        {
            await _salesOrderRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
            await _salesOrderItemRepository.DeleteAsync(m => param.Ids.Any(n => n == m.SalesOrderId));
        }
    }
}

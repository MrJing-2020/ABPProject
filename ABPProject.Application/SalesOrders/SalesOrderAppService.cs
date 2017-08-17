using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ABPProject.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABPProject.Extend;
using ABPProject.SalesOrders.Dto;
using Abp.Domain.Uow;
using ABPProject.Authorization;
using Abp.Authorization;
using ABPProject.InventSites;
using ABPProject.Contracts;
using ABPProject.Clients;

namespace ABPProject.SalesOrders
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class SalesOrderAppService: ABPProjectAppServiceBase, ISalesOrderAppService
    {
        private readonly SalesOrderManager _salesOrderManager;
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        private readonly IRepository<SalesOrderItem, int> _salesOrderItemRepository;
        private readonly IRepository<InventSite, int> _inventSiteRepository;
        private readonly IRepository<Contract, int> _contractRepository;
        private readonly IRepository<Client, int> _clientRepository;


        public SalesOrderAppService(
            SalesOrderManager salesOrderManager, 
            IRepository<SalesOrder, int> salesOrderRepository,
            IRepository<SalesOrderItem, int> salesOrderItemRepository,
            IRepository<InventSite, int> inventSiteRepository,
            IRepository<Contract, int> contractRepository,
            IRepository<Client, int> clientRepository
            )
        {
            _salesOrderManager = salesOrderManager;
            _salesOrderRepository = salesOrderRepository;
            _salesOrderItemRepository = salesOrderItemRepository;
            _inventSiteRepository = inventSiteRepository;
            _contractRepository = contractRepository;
            _clientRepository = clientRepository;
        }

        public PagedResultDto<SalesOrderListDto> GetPagedSalesOrder(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<SalesOrder> salesOrder = _salesOrderRepository.GetAllIncluding(m => m.SalesOrderItem);
            //字段搜索
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                salesOrder = salesOrder.Where(m => m.SalesNum.Contains(input.SearchText));
                count = salesOrder.Count();
            }
            else
            {
                count = _salesOrderRepository.Count();
            }
            //字段排序（支持主表中的所有字段）
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
            //连表查询
            var resultList = (from order in salesOrder
                              join client in _clientRepository.GetAll()
                              on order.ClientId equals client.Id
                              select new SalesOrderListDto
                              {
                                  Id = order.Id,
                                  SalesNum = order.SalesNum,
                                  ClientName = client.Name,
                                  ClientId = order.ClientId,
                                  InventSiteId = order.InventSiteId,
                                  InventLocationId = order.InventLocationId,
                                  SalesContractId = order.SalesContractId,
                                  DeliveryDate = order.DeliveryDate,
                                  Consignee = order.Consignee,
                                  DeliveryAddress = order.DeliveryAddress,
                                  PostCode = order.PostCode,
                                  DistributionMode = order.DistributionMode,
                                  MobilePhone = order.MobilePhone,
                                  InvoiceHeader = order.InvoiceHeader,
                                  Instructions = order.Instructions,
                                  PaymentMethod = order.PaymentMethod
                              }).ToList();
            //附表数据映射
            foreach (var item in resultList)
            {
                item.SalesOrderItems = salesOrder.Where(m => m.Id == item.Id).FirstOrDefault().SalesOrderItem.MapTo<List<SalesOrderItemListDto>>();
            }
            return new PagedResultDto<SalesOrderListDto>(count, resultList);
        }

        public async Task<object> GetSelectList()
        {
            var client = (await _clientRepository.GetAllListAsync()).Select(m => new { Id = m.Id, Name = m.Name });
            var contract = (await _contractRepository.GetAllListAsync()).Select(m => new { Id = m.Id, Name = m.Name });
            var inventSiteList = _inventSiteRepository.GetAllIncluding(m => m.InventLocation).ToList();
            var inventSite = inventSiteList.MapTo<List<InventSiteDto>>();
            foreach (var item in inventSite)
            {
                item.InventLocations = inventSiteList.Where(m => m.Id == item.Id).FirstOrDefault().InventLocation.MapTo<List<InventLocationDto>>();
            }
            return new { client = client, contract = contract, inventSite = inventSite };
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

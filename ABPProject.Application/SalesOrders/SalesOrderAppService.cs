using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using ABPProject.CommonDto;
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
using ABPProject.Products;
using ABPProject.Products.Dto;

namespace ABPProject.SalesOrders
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class SalesOrderAppService: ABPProjectAppServiceBase, ISalesOrderAppService
    {
        private readonly SalesOrderManager _salesOrderManager;
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        private readonly IRepository<SalesOrderItem, int> _salesOrderItemRepository;
        private readonly IRepository<InventSite, int> _inventSiteRepository;
        private readonly IRepository<InventLocation, int> _inventLocationRepository;
        private readonly IRepository<InventBatch, int> _inventBatchRepository;
        private readonly IRepository<Contract, int> _contractRepository;
        private readonly IRepository<Client, int> _clientRepository;
        private readonly IRepository<Product, int> _productRepository;

        public SalesOrderAppService(
            SalesOrderManager salesOrderManager,
            IRepository<SalesOrder, int> salesOrderRepository,
            IRepository<SalesOrderItem, int> salesOrderItemRepository,
            IRepository<InventSite, int> inventSiteRepository,
            IRepository<Contract, int> contractRepository,
            IRepository<Client, int> clientRepository,
            IRepository<Product, int> productRepository,
            IRepository<InventLocation, int> inventLocationRepository,
            IRepository<InventBatch, int> inventBatchRepository
            )
        {
            _salesOrderManager = salesOrderManager;
            _salesOrderRepository = salesOrderRepository;
            _salesOrderItemRepository = salesOrderItemRepository;
            _inventSiteRepository = inventSiteRepository;
            _contractRepository = contractRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _inventLocationRepository = inventLocationRepository;
            _inventBatchRepository = inventBatchRepository;
        }

        public PagedResultDto<SalesOrderListDto> GetPagedSalesOrder(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<SalesOrder> salesOrder = _salesOrderRepository.GetAllIncluding(m => m.SalesOrderItem);
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from order in salesOrder
                        join client in _clientRepository.GetAll() on order.ClientId equals client.Id
                        join contract in _contractRepository.GetAll() on order.SalesContractId equals contract.Id
                        where (isSearch ? order.SalesNum.Contains(input.SearchText) || client.Name.Contains(input.SearchText) : true)
                        select new SalesOrderListDto
                        {
                            Id = order.Id,
                            SalesNum = order.SalesNum,
                            ClientName = client.Name,
                            ContractNum = contract.Name,
                            CreationTime = order.CreationTime
                        }).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
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
            var productList = _productRepository.GetAllIncluding(m => m.InventBatch).ToList();
            var product = productList.Select(m => new ProductSelectList { Id=m.Id,Name=m.Name, InventBatchs = m.InventBatch.MapTo<List<InventBatchDto>>() });
            foreach (var item in inventSite)
            {
                item.InventLocations = inventSiteList.Where(m => m.Id == item.Id).FirstOrDefault().InventLocation.MapTo<List<InventLocationDto>>();
            }
            return new { client = client, contract = contract, inventSite = inventSite, product = product };
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
           var salesOrderItems = input.SalesOrderItems.MapTo<List<SalesOrderItem>>();
           var salesOrderUpdate= await _salesOrderRepository.InsertOrUpdateAsync(salesOrder);
            foreach (var item in salesOrderItems)
            {
                item.SalesOrderId = salesOrderUpdate.Id;
                await _salesOrderItemRepository.InsertOrUpdateAsync(item);
            }
        }

        public async Task<SalesOrderDetailDto> SalesOrderDetail(int id)
        {
            var salesOrderDetail = (from salesOrder in _salesOrderRepository.GetAll()
                                    join client in _clientRepository.GetAll() on salesOrder.ClientId equals client.Id
                                    join contract in _contractRepository.GetAll() on salesOrder.SalesContractId equals contract.Id
                                    join inventSite in _inventSiteRepository.GetAll() on salesOrder.InventSiteId equals inventSite.Id
                                    join inventLocation in _inventLocationRepository.GetAll() on salesOrder.InventLocationId equals inventLocation.Id
                                    where salesOrder.Id == id
                                    select new SalesOrderDetailDto
                                    {
                                        Id = salesOrder.Id,
                                        SalesNum = salesOrder.SalesNum,
                                        ClientName = client.Name,
                                        InventSiteName = inventSite.Name,
                                        InventLocationName = inventLocation.Name,
                                        SalesContractNum = contract.Name,
                                        Consignee = salesOrder.Consignee,
                                        DeliveryAddress = salesOrder.DeliveryAddress,
                                        PostCode = salesOrder.PostCode,
                                        DistributionMode = salesOrder.DistributionMode,
                                        MobilePhone = salesOrder.MobilePhone,
                                        InvoiceHeader = salesOrder.InvoiceHeader,
                                        Instructions = salesOrder.Instructions,
                                        PaymentMethod = salesOrder.PaymentMethod,
                                        State = salesOrder.State,
                                        CreationTime = salesOrder.CreationTime,
                                        CreateUserId = salesOrder.CreatorUserId
                                    }).FirstOrDefault();
            var user = await UserManager.GetUserByIdAsync((long)salesOrderDetail.CreateUserId);
            salesOrderDetail.CreatorUserName = user.UserName;
            var salesOrderItems = (from salesOrderItem in _salesOrderItemRepository.GetAll()
                                  join product in _productRepository.GetAll() on salesOrderItem.ProductId equals product.Id
                                  join inventBatchItem in _inventBatchRepository.GetAll() on salesOrderItem.InventBatchId equals inventBatchItem.Id
                                  where salesOrderItem.SalesOrderId == salesOrderDetail.Id
                                  select new SalesOrderItemDetailDto
                                  {
                                      ProductName = product.Name,
                                      InventBatchNum = inventBatchItem.InventBatchNum,
                                      PurchCount = salesOrderItem.PurchCount,
                                      PurchPrice = salesOrderItem.PurchPrice
                                  }).ToList();
            salesOrderDetail.SalesOrderItems = salesOrderItems;
            return salesOrderDetail;
        }

        [UnitOfWork]
        public async Task DeleteSalesOrder(ArrayParams param)
        {
            await _salesOrderRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
            await _salesOrderItemRepository.DeleteAsync(m => param.Ids.Any(n => n == m.SalesOrderId));
        }
    }
}

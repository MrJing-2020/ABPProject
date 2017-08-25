using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ABPProject.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using ABPProject.PurchaseOrders.Dto;
using Abp.Authorization;
using ABPProject.Authorization;
using ABPProject.Suppliers;
using ABPProject.Contracts;
using ABPProject.InventSites;
using ABPProject.SalesOrders.Dto;
using ABPProject.Products;
using ABPProject.Products.Dto;
using ABPProject.Users;

namespace ABPProject.PurchaseOrders
{
    [AbpAuthorize(PermissionNames.PurchaseOrder)]
    public class PurchaseOrderAppService : ABPProjectAppServiceBase, IPurchaseOrderAppService
    {
        private readonly IRepository<PurchaseOrder, int> _purchaseOrderRepository;
        private readonly IRepository<PurchaseOrderItem, int> _purchaseOrderItemRepository;
        private readonly IRepository<Supplier, int> _supplierRepository;
        private readonly IRepository<Contract, int> _contractRepository;
        private readonly IRepository<InventSite, int> _inventSiteRepository;
        private readonly IRepository<InventLocation, int> _inventLocationRepository;
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<InventBatch, int> _inventBatchRepository;


        public PurchaseOrderAppService(
            IRepository<PurchaseOrder, int> purchaseOrderRepository,
            IRepository<PurchaseOrderItem, int> purchaseOrderItemRepository,
            IRepository<Supplier, int> supplierRepository,
            IRepository<Contract, int> contractRepository,
            IRepository<InventSite, int> inventSiteRepository,
            IRepository<InventLocation, int> inventLocationRepository,
            IRepository<Product, int> productRepository,
            IRepository<User, long> userRepository,
            IRepository<InventBatch, int> inventBatchRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
            _supplierRepository = supplierRepository;
            _contractRepository = contractRepository;
            _inventSiteRepository = inventSiteRepository;
            _inventLocationRepository = inventLocationRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _inventBatchRepository = inventBatchRepository;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageArg"></param>
        /// <returns></returns>
        public PagedResultDto<PurchaseOrderListDto> GetPagedPurchaseOrder(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<PurchaseOrder> purchaseOrder = _purchaseOrderRepository.GetAllIncluding(m => m.PurchaseOrderItem);
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            var list = (from order in purchaseOrder
                        join contract in _contractRepository.GetAll() on order.ContractId equals contract.Id
                        join supplier in _supplierRepository.GetAll() on order.SupplierId equals supplier.Id
                        where (isSearch ? order.PurchNum.Contains(input.SearchText) || supplier.Name.Contains(input.SearchText) : true)
                        select new PurchaseOrderListDto
                        {
                            Id = order.Id,
                            PurchNum = order.PurchNum,
                            SupplierName = supplier.Name,
                            DeliveryDate = order.DeliveryDate,
                            State = order.State,
                            ContractNum = contract.ContractNum,
                            CreationTime = order.CreationTime
                        }).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
            //附表数据映射
            foreach (var item in resultList)
            {
                item.PurchaseOrderItems = purchaseOrder.Where(m => m.Id == item.Id).FirstOrDefault().PurchaseOrderItem.MapTo<List<PurchaseOrderItemListDto>>();
            }
            return new PagedResultDto<PurchaseOrderListDto>(count, resultList);
        }

        public async Task<object> GetSelectList()
        {
            var supplier = (await _supplierRepository.GetAllListAsync()).Select(m => new { Id = m.Id, Name = m.Name });
            var contract = (await _contractRepository.GetAllListAsync()).Select(m=>new { Id=m.Id,Name=m.ContractNum});
            var inventSiteList = _inventSiteRepository.GetAllIncluding(m => m.InventLocation).ToList();
            var inventSite = inventSiteList.MapTo<List<InventSiteDto>>();
            var productList = _productRepository.GetAllIncluding(m => m.InventBatch).ToList();
            var product = productList.Select(m => new ProductSelectList { Id = m.Id, Name = m.Name, InventBatchs = m.InventBatch.MapTo<List<InventBatchDto>>() });
            foreach (var item in inventSite)
            {
                item.InventLocations = inventSiteList.Where(m => m.Id == item.Id).FirstOrDefault().InventLocation.MapTo<List<InventLocationDto>>();
            }
            return new { supplier = supplier, contract = contract, inventSite = inventSite, product = product };
        }

        public EditPurchaseOrderInput GetPurchaseOrderById(OneParam param)
        {
            var purchaseOrder = _purchaseOrderRepository.GetAllIncluding(m => m.PurchaseOrderItem).Where(m => m.Id == param.Id).FirstOrDefault();
            var purchaseOrderDto = purchaseOrder.MapTo<EditPurchaseOrderInput>();
            purchaseOrderDto.PurchaseOrderItems = purchaseOrder.PurchaseOrderItem.MapTo<List<EditPurchaseOrderItemDto>>();
            return purchaseOrderDto;
        }

        public async Task EditPurchaseOrder(EditPurchaseOrderInput input)
        {
            var purchaseOrder = input.MapTo<PurchaseOrder>();
            purchaseOrder.DeliveryDate = DateTime.Now;
            var purchaseOrderItems = input.PurchaseOrderItems.MapTo<List<PurchaseOrderItem>>();
            await _purchaseOrderRepository.InsertOrUpdateAsync(purchaseOrder);
            foreach (var item in purchaseOrderItems)
            {
                await _purchaseOrderItemRepository.InsertOrUpdateAsync(item);
            }
        }
        public PurchaseOrderDetailDto PurchaseOrderDetail(OneParam param)
        {
            //订单详情
            var purchaseOrderDetail = (from purchaseOrder in _purchaseOrderRepository.GetAll()
                                       join contract in _contractRepository.GetAll() on purchaseOrder.ContractId equals contract.Id
                                       join supplier in _supplierRepository.GetAll() on purchaseOrder.SupplierId equals supplier.Id
                                       join inventSite in _inventSiteRepository.GetAll() on purchaseOrder.InventSiteId equals inventSite.Id
                                       join inventLocation in _inventLocationRepository.GetAll() on purchaseOrder.InventLocationId equals inventLocation.Id
                                       join userInfo in _userRepository.GetAll() on purchaseOrder.CreatorUserId equals userInfo.Id
                                       where purchaseOrder.Id == param.Id
                                       select new PurchaseOrderDetailDto
                                       {
                                           Id = purchaseOrder.Id,
                                           PurchNum = purchaseOrder.PurchNum,
                                           SupplierName = supplier.Name,
                                           InventSiteName = inventSite.Name,
                                           InventLocationName = inventLocation.Name,
                                           ContractNum = contract.ContractNum,
                                           State = purchaseOrder.State,
                                           CreationTime = purchaseOrder.CreationTime,
                                           CreatorUserName = userInfo.UserName
                                       }).FirstOrDefault();
            //订单行
            var purchaseOrderItems = (from purchaseOrderItem in _purchaseOrderItemRepository.GetAll()
                                      join product in _productRepository.GetAll() on purchaseOrderItem.ProductId equals product.Id
                                      join inventBatchItem in _inventBatchRepository.GetAll() on purchaseOrderItem.InventBatchId equals inventBatchItem.Id
                                      where purchaseOrderItem.PurchaseOrderId == purchaseOrderDetail.Id
                                      select new PurchaseOrderItemDetailDto
                                      {
                                          ProductName = product.Name,
                                          InventBatchNum = inventBatchItem.InventBatchNum,
                                          PurchCount = purchaseOrderItem.PurchCount,
                                          PurchPrice = purchaseOrderItem.PurchPrice
                                      }).ToList();
            purchaseOrderDetail.PurchaseOrderItems = purchaseOrderItems;
            //商品总价
            purchaseOrderDetail.TotalPrices = purchaseOrderItems.Sum(m => (m.PurchCount) * (m.PurchPrice));
            return purchaseOrderDetail;
        }

        [UnitOfWork]
        public async Task DeletePurchaseOrder(ArrayParams param)
        {
            await _purchaseOrderRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
            await _purchaseOrderItemRepository.DeleteAsync(m => param.Ids.Any(n => n == m.PurchaseOrderId));
        }
    }
}

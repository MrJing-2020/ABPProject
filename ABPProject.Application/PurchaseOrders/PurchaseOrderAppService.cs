using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ABPProject.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using ABPProject.PurchaseOrders.Dto;
using Abp.Authorization;
using ABPProject.Authorization;

namespace ABPProject.PurchaseOrders
{
    [AbpAuthorize(PermissionNames.PurchaseOrder)]
    public class PurchaseOrderAppService : ABPProjectAppServiceBase, IPurchaseOrderAppService
    {
        private readonly IRepository<PurchaseOrder, int> _purchaseOrderRepository;
        private readonly IRepository<PurchaseOrderItem, int> _purchaseOrderItemRepository;

        public PurchaseOrderAppService(
            IRepository<PurchaseOrder, int> purchaseOrderRepository,
            IRepository<PurchaseOrderItem, int> purchaseOrderItemRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderItemRepository = purchaseOrderItemRepository;
        }

        public PagedResultDto<PurchaseOrderListDto> GetPagedPurchaseOrder(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<PurchaseOrder> purchaseOrder = _purchaseOrderRepository.GetAllIncluding(m => m.PurchaseOrderItem);
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                purchaseOrder = purchaseOrder.Where(m => m.PurchName.Contains(input.SearchText) || m.PurchId.Contains(input.SearchText));
                count = purchaseOrder.Count();
            }
            else
            {
                count = _purchaseOrderRepository.Count();
            }
            if (!string.IsNullOrEmpty(input.SortName))
            {
                //将首字母转成大写
                input.SortName = input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1);
                purchaseOrder = input.SortOrder == "desc" ? purchaseOrder.OrderBy(input.SortName, true).PageBy(input.PageInput) :
                purchaseOrder.OrderBy(input.SortName).PageBy(input.PageInput);
            }
            else
            {
                //默认按时间降序
                purchaseOrder = purchaseOrder.OrderByDescending(m => m.CreationTime).PageBy(input.PageInput);
            }
            var resultList = purchaseOrder.ToList().MapTo<List<PurchaseOrderListDto>>();
            foreach (var item in resultList)
            {
                item.PurchaseOrderItems = purchaseOrder.Where(m => m.Id == item.Id).FirstOrDefault().PurchaseOrderItem.MapTo<List<PurchaseOrderItemListDto>>();
            }
            return new PagedResultDto<PurchaseOrderListDto>(count, resultList);
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

        [UnitOfWork]
        public async Task DeletePurchaseOrder(ArrayParams param)
        {
            await _purchaseOrderRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
            await _purchaseOrderItemRepository.DeleteAsync(m => param.Ids.Any(n => n == m.PurchaseOrderId));
        }
    }
}

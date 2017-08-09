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

namespace ABPProject.SalesOrders
{
    public class SalesOrderAppService: ABPProjectAppServiceBase, ISalesOrderAppService
    {
        private readonly SalesOrderManager _salesOrderManager;
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        public SalesOrderAppService(SalesOrderManager salesOrderManager, IRepository<SalesOrder, int> salesOrderRepository)
        {
            _salesOrderManager = salesOrderManager;
            _salesOrderRepository = salesOrderRepository;
        }

        public PagedResultDto<SalesOrderListDto> GetPagedSalesOrder(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<SalesOrder> salesOrder = null;
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                salesOrder = _salesOrderRepository.GetAll().Where(m => m.SalesName.Contains(input.SearchText) || m.SalesId.Contains(input.SearchText));
                count = salesOrder.Count();
            }
            else
            {
                salesOrder = _salesOrderRepository.GetAll();
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
            return new PagedResultDto<SalesOrderListDto>(count, salesOrder.MapTo<List<SalesOrderListDto>>());
        }

        public async Task<EditSalesOrderInput> GetSalesOrderById(OneParam param)
        {
            var salesOrder = await _salesOrderRepository.GetAsync(param.Id);
            return salesOrder.MapTo<EditSalesOrderInput>();
        }

        //public async Task EditSalesOrder(EditSalesOrderInput input)
        //{
        //    var id = input.Id;
        //    if (id == null)
        //    {
        //        var salesOrder = input.MapTo<SalesOrder>();
        //        await _salesOrderRepository.InsertAsync(salesOrder);
        //    }
        //    else
        //    {
        //        var oldSalesOrder = await _salesOrderRepository.GetAsync((int)id);
        //        oldSalesOrder.Name = input.Name;
        //        oldSalesOrder.Description = input.Description;
        //    }
        //}

        public async Task DeleteSalesOrder(ArrayParams param)
        {
            await _salesOrderRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}

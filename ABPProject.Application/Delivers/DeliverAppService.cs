using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using ABPProject.CommonDto;
using ABPProject.SalesOrders;
using System.Linq;
using System.Threading.Tasks;
using ABPProject.Extend;
using ABPProject.Delivers.Dto;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;

namespace ABPProject.Delivers
{
    /// <summary>
    /// 发货单相关服务
    /// </summary>
    public class DeliverAppService: ABPProjectAppServiceBase, IDeliverAppService
    {
        private readonly IRepository<Deliver, int> _deliverRepository;
        private readonly IRepository<SalesOrder, int> _salesOrderRepository;
        public DeliverAppService(IRepository<Deliver, int> deliverRepository,
            IRepository<SalesOrder, int> salesOrderRepository)
        {
            _deliverRepository = deliverRepository;
            _salesOrderRepository = salesOrderRepository;
        }

        public PagedResultDto<DeliverListDto> GetPagedDeliver(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<Deliver> deliver = _deliverRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "DeliverTime";
            //连表查询
            var list = (from deliverItem in deliver
                        join salesOrder in _salesOrderRepository.GetAll() on deliverItem.SalesOrderId equals salesOrder.Id
                        where (isSearch ? deliverItem.LogisticsNum.Contains(input.SearchText) || salesOrder.SalesNum.Contains(input.SearchText) : true)
                        select new DeliverListDto
                        {
                            Id = deliverItem.Id,
                            SalesOrderNum = salesOrder.SalesNum,
                            LogisticsCompany = deliverItem.LogisticsCompany,
                            LogisticsNum = deliverItem.LogisticsNum,
                            DeliverTime = deliverItem.DeliverTime
                        }).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
            return new PagedResultDto<DeliverListDto>(count, resultList);
        }

        public async Task<EditDeliverInput> GetDeliverById(OneParam param)
        {
            var deliver = await _deliverRepository.GetAsync(param.Id);
            return deliver.MapTo<EditDeliverInput>();
        }

        public async Task<object> GetSelectList()
        {
            var salesOrder = await _salesOrderRepository.GetAllListAsync();
            return new { SalesOrder = salesOrder.Select(m => new { Id = m.Id, SalesOrderNum = m.SalesNum }) };
        }

        [UnitOfWork]
        public async Task EditDeliver(EditDeliverInput input)
        {
            //加上时区差
            input.DeliverTime = input.DeliverTime.AddHours(8);
            var deliver = input.MapTo<Deliver>();
            await _deliverRepository.InsertOrUpdateAsync(deliver);
            //更改销售订单状态
            //   ...change state
        }

        public async Task DeleteDeliver(ArrayParams param)
        {
            await _deliverRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.SalesOrders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.SalesOrders
{
    public interface ISalesOrderAppService: IApplicationService
    {
        PagedResultDto<SalesOrderListDto> GetPagedSalesOrder(PageParams pageArg);
        Task DeleteSalesOrder(ArrayParams param);
        EditSalesOrderInput GetSalesOrderById(OneParam param);
        Task EditSalesOrder(EditSalesOrderInput input);
        Task<object> GetSelectList();
    }
}

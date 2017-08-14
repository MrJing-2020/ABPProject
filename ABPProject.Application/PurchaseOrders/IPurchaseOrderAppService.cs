using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.PurchaseOrders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPProject.PurchaseOrders
{
    public interface IPurchaseOrderAppService: IApplicationService
    {
        PagedResultDto<PurchaseOrderListDto> GetPagedPurchaseOrder(PageParams pageArg);
        EditPurchaseOrderInput GetPurchaseOrderById(OneParam param);
        Task EditPurchaseOrder(EditPurchaseOrderInput input);
        Task DeletePurchaseOrder(ArrayParams param);
    }
}

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.Receipts.Dto;
using System.Threading.Tasks;

namespace ABPProject.Receipts
{
    public interface IReceiptAppService: IApplicationService
    {
        PagedResultDto<ReceiptListDto> GetPagedReceipt(PageParams pageArg);
        Task<EditReceiptInput> GetReceiptById(OneParam param);
        Task EditReceipt(EditReceiptInput input);
        Task DeleteReceipt(ArrayParams param);
        Task<object> GetSelectList();
        ReceiptDedailDto ReceiptDetail(OneParam param);
    }
}

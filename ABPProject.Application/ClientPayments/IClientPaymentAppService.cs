using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.ClientPayments.Dto;
using ABPProject.CommonDto;
using System.Threading.Tasks;

namespace ABPProject.ClientPayments
{
    public interface IClientPaymentAppService: IApplicationService
    {
        PagedResultDto<ClientPaymentDto> GetPagedClientPayment(PageParams pageArg);
        ClientPaymentDto ClientPaymentDetail(OneParam param);
    }
}

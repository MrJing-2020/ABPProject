using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.Delivers.Dto;
using System.Threading.Tasks;

namespace ABPProject.Delivers
{
    public interface IDeliverAppService: IApplicationService
    {
        PagedResultDto<DeliverListDto> GetPagedDeliver(PageParams pageArg);
        Task<EditDeliverInput> GetDeliverById(OneParam param);
        Task<object> GetSelectList();
        Task EditDeliver(EditDeliverInput input);
        Task DeleteDeliver(ArrayParams param);
    }
}

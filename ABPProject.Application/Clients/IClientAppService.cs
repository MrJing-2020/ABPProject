using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.Clients.Dto;
using ABPProject.CommonDto;
using System.Threading.Tasks;

namespace ABPProject.Clients
{
    public interface IClientAppService: IApplicationService
    {
        PagedResultDto<ClientListDto> GetPagedClient(PageParams pageArg);
        Task<EditClientInput> GetClientById(OneParam param);
        Task EditClient(EditClientInput input);
        Task DeleteClient(ArrayParams param);
    }
}

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPProject.CommonDto;
using ABPProject.Projects.Dto;
using System.Threading.Tasks;

namespace ABPProject.Projects
{
    public interface IProjectAppService: IApplicationService
    {
        Task<ListResultDto<ProjectListDto>> GetProjects();
        Task EditProject(EditProjectInput input);
        Task DeleteProject(ArrayParams param);
        PagedResultDto<ProjectListDto> GetPagedProject(PageParams pageArg);
        Task<EditProjectInput> GetProjectById(OneParam param);
    }
}

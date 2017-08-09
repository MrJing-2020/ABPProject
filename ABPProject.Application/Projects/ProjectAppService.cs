using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABPProject.Projects.Dto;
using Abp.AutoMapper;
using ABPProject.CommonDto;
using ABPProject.Extend;
using Abp.Linq.Extensions;
using Abp.Authorization;
using ABPProject.Authorization;

namespace ABPProject.Projects
{
    [AbpAuthorize(PermissionNames.Project)]
    public class ProjectAppService: ABPProjectAppServiceBase, IProjectAppService
    {
        private readonly ProjectManager _projectManager;
        private readonly IRepository<Project, int> _projectRepository;

        public ProjectAppService(ProjectManager projectManager, IRepository<Project, int> projectRepository)
        {
            _projectManager = projectManager;
            _projectRepository = projectRepository;
        }

        public async Task<ListResultDto<ProjectListDto>> GetProjects()
        {
            var projects = await _projectRepository.GetAllListAsync();
            return new ListResultDto<ProjectListDto>(
                projects.MapTo<List<ProjectListDto>>()
            );
        }

        public PagedResultDto<ProjectListDto> GetPagedProject(PageParams pageArg)
        {
            PageArgDto input = new PageArgDto(pageArg);
            int count = 0;
            IQueryable<Project> project = null;
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                project = _projectRepository.GetAll().Where(m => m.Name.Contains(input.SearchText) || m.Description.Contains(input.SearchText));
                count = project.Count();
            }
            else
            {
                project = _projectRepository.GetAll();
                count = _projectRepository.Count();
            }
            if (!string.IsNullOrEmpty(input.SortName))
            {
                //将首字母转成大写
                input.SortName = input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1);
                project = input.SortOrder == "desc" ? project.OrderBy(input.SortName, true).PageBy(input.PageInput) :
                project.OrderBy(input.SortName).PageBy(input.PageInput);
            }
            else
            {
                //默认按时间降序
                project = project.OrderByDescending(m => m.CreationTime).PageBy(input.PageInput);
            }
            return new PagedResultDto<ProjectListDto>(count, project.MapTo<List<ProjectListDto>>());
        }

        public async Task<EditProjectInput> GetProjectById(OneParam param)
        {
            var project = await _projectRepository.GetAsync(param.Id);
            return project.MapTo<EditProjectInput>();
        }

        public async Task EditProject(EditProjectInput input)
        {
            var id = input.Id;
            if (id == null)
            {
                var project = input.MapTo<Project>();
                await _projectRepository.InsertAsync(project);
            }
            else
            {
                var oldProject = await _projectRepository.GetAsync((int)id);
                oldProject.Name = input.Name;
                oldProject.Description = input.Description;
                await _projectRepository.UpdateAsync(oldProject);
            }
        }

        public async Task DeleteProject(ArrayParams param)
        {
            await _projectRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}

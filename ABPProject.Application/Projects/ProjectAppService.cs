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
            bool isSearch = !string.IsNullOrEmpty(input.SearchText);
            IQueryable<Project> project = _projectRepository.GetAll();
            bool orderByDesc = input.SortOrder == "desc" ? true : false;
            string sortName = !string.IsNullOrEmpty(input.SortName) ? input.SortName.Substring(0, 1).ToUpper() + input.SortName.Substring(1) : "CreationTime";
            //连表查询
            var list = (from projectItem in project
                        where (isSearch ? projectItem.Name.Contains(input.SearchText) || projectItem.Description.Contains(input.SearchText) : true)
                        select projectItem).OrderBy(sortName, orderByDesc);
            int count = list.Count();
            var resultList = list.PageBy(input.PageInput).ToList();
            return new PagedResultDto<ProjectListDto>(count, resultList.MapTo<List<ProjectListDto>>());
        }

        public async Task<EditProjectInput> GetProjectById(OneParam param)
        {
            var project = await _projectRepository.GetAsync(param.Id);
            return project.MapTo<EditProjectInput>();
        }

        public async Task EditProject(EditProjectInput input)
        {
            //加上时区差
            input.BeginDate = input.BeginDate.AddHours(8);
            var project = input.MapTo<Project>();
            await _projectRepository.InsertOrUpdateAsync(project);

        }

        public async Task DeleteProject(ArrayParams param)
        {
            await _projectRepository.DeleteAsync(m => param.Ids.Any(n => n == m.Id));
        }
    }
}

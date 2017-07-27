using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
using Abp.Authorization;
using ABPProject.Roles;
using ABPProject.CommonDto;

namespace ABPProject.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class RolesController : ABPProjectControllerBase
    {
        private readonly IRoleAppService _roleAppService;
        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }
        public async Task<ActionResult> Index()
        {
            var output = await _roleAppService.GetRoles();
            return View(output);
        }

        public ActionResult PagedList(int? page)
        {
            var pageSize = 5;
            var pageNumber = page ?? 1;
            var pageArg = new PagedInputDto
            {
                SkipCount = (pageNumber - 1) * pageSize, 
                MaxResultCount = pageSize
            };
            var result = _roleAppService.GetPagedRole(pageArg);
            return View(result);
        }
    }
}
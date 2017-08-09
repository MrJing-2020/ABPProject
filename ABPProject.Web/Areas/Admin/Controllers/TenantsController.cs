using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
using ABPProject.MultiTenancy;
using ABPProject.Web.Controllers;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class TenantsController : ABPProjectControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}
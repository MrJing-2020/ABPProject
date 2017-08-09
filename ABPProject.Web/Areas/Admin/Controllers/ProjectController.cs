using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using ABPProject.Projects;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Project)]
    public class ProjectController : ABPProjectControllerBase
    {
        private readonly IProjectAppService _projectAppService;
        public ProjectController(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        // GET: Admin/Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _projectAppService.GetPagedProject(pageArg);
            return Json(result);
        }
    }
}
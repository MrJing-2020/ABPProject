using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using ABPProject.Delivers;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize(PermissionNames.PurchaseOrder)]
    public class DeliverController : ABPProjectControllerBase
    {
        private readonly IDeliverAppService _deliverAppService;
        public DeliverController(IDeliverAppService deliverAppService)
        {
            _deliverAppService = deliverAppService;
        }

        // GET: Admin/Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _deliverAppService.GetPagedDeliver(pageArg);
            return Json(result);
        }
    }
}
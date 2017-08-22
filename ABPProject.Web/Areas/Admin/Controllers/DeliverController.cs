using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
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
        public ActionResult Index()
        {
            return View();
        }
    }
}
using Abp.Web.Mvc.Authorization;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ABPProjectControllerBase
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
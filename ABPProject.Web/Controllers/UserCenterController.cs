using Abp.Web.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Controllers
{
    /// <summary>
    /// 用户个人中心
    /// </summary>
    [AbpMvcAuthorize]
    public class UserCenterController : ABPProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
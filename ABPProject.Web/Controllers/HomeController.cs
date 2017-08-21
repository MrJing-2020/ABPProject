using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace ABPProject.Web.Controllers
{
    //[AbpMvcAuthorize]
    public class HomeController : ABPProjectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeHead()
        {
            return View();
        }

        public ActionResult HomeFooter()
        {
            return View();
        }
	}
}
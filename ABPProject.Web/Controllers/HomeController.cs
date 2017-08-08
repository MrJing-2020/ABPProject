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
	}
}
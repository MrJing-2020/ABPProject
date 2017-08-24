using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using ABPProject.Sessions;
using Abp.Threading;
using ABPProject.Sessions.Dto;

namespace ABPProject.Web.Controllers
{
    //[AbpMvcAuthorize]
    public class HomeController : ABPProjectControllerBase
    {
        private readonly ISessionAppService _sessionAppService;
        public HomeController(ISessionAppService sessionAppService)
        {
            _sessionAppService = sessionAppService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeHead()
        {
            bool isLogin = AbpSession.UserId == null ? false : true;
            GetCurrentLoginInformationsOutput userInfo = new GetCurrentLoginInformationsOutput();
            if (isLogin)
            {
                userInfo = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());
            }
            ViewBag.IsLogin = isLogin;
            return PartialView(userInfo);
        }

        public ActionResult HomeFooter()
        {
            return PartialView();
        }
	}
}
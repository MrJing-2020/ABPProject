using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using ABPProject.Sessions;
using ABPProject.Web.Models.Layout;
using System;
using ABPProject.Sessions.Dto;

namespace ABPProject.Web.Controllers
{
    public class LayoutController : ABPProjectControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public LayoutController(
            IUserNavigationManager userNavigationManager, 
            ISessionAppService sessionAppService, 
            IMultiTenancyConfig multiTenancyConfig)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        [ChildActionOnly]
        public PartialViewResult Nav(string activeMenu = "")
        {
            var loginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());
            var menu = new TopMenuViewModel
                        {
                            MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                            ActiveMenuItemName = activeMenu
                        };

            return PartialView("_Nav", Tuple.Create<TopMenuViewModel, GetCurrentLoginInformationsOutput>(menu, loginInformations));
        }

        [ChildActionOnly]
        public PartialViewResult TopBar()
        {
            return PartialView("_TopBar");
        }

        [ChildActionOnly]
        public PartialViewResult Right()
        {
            return PartialView("_Right");
        }
        [ChildActionOnly]
        public PartialViewResult ProductTop()
        {
            return PartialView("_ProductTop");
        }
        [ChildActionOnly]
        public PartialViewResult Chatbox()
        {
            return PartialView("_Chat");
        }
    }
}
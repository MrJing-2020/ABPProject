using ABPProject.Clients;
using ABPProject.CommonDto;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    public class ClientController : ABPProjectControllerBase
    {
        private readonly IClientAppService _clientAppService;
        public ClientController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _clientAppService.GetPagedClient(pageArg);
            return Json(result);
        }
    }
}
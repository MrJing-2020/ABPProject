using Abp.Authorization;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using ABPProject.SalesOrders;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class SalesOrderController : ABPProjectControllerBase
    {
        private readonly ISalesOrderAppService _salesOrderAppService;
        public SalesOrderController(ISalesOrderAppService salesOrderAppService)
        {
            _salesOrderAppService = salesOrderAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _salesOrderAppService.GetPagedSalesOrder(pageArg);
            return Json(result);
        }

        public ActionResult Detail(int id = 3)
        {
            return View();
        }
    }
}
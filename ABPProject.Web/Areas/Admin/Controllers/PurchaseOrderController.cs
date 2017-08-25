using Abp.Authorization;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using ABPProject.PurchaseOrders;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpAuthorize(PermissionNames.PurchaseOrder)]
    public class PurchaseOrderController : ABPProjectControllerBase
    {
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;
        public PurchaseOrderController(IPurchaseOrderAppService purchaseOrderAppService)
        {
            _purchaseOrderAppService = purchaseOrderAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _purchaseOrderAppService.GetPagedPurchaseOrder(pageArg);
            return Json(result);
        }
        public ActionResult Detail(int id)
        {
            var purchaseOrderDetail = _purchaseOrderAppService.PurchaseOrderDetail(new OneParam { Id = id });
            return View(purchaseOrderDetail);
        }
    }
}
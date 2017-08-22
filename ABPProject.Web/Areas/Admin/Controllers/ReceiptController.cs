using Abp.Authorization;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using ABPProject.Receipts;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpAuthorize(PermissionNames.SalesOrder)]
    public class ReceiptController : ABPProjectControllerBase
    {
        private readonly IReceiptAppService _receiptOrderAppService;
        public ReceiptController(IReceiptAppService receiptOrderAppService)
        {
            _receiptOrderAppService = receiptOrderAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _receiptOrderAppService.GetPagedReceipt(pageArg);
            return Json(result);
        }
    }
}
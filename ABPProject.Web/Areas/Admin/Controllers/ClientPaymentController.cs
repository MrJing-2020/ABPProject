using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
using ABPProject.ClientPayments;
using ABPProject.CommonDto;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 客户付款
    /// </summary>
    [AbpMvcAuthorize(PermissionNames.PurchaseOrder)]
    public class ClientPaymentController : ABPProjectControllerBase
    {
        private readonly IClientPaymentAppService _clientPaymentAppService;
        public ClientPaymentController(IClientPaymentAppService clientPaymentAppService)
        {
            _clientPaymentAppService = clientPaymentAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _clientPaymentAppService.GetPagedClientPayment(pageArg);
            return Json(result);
        }

        public ActionResult Detail(int id)
        {
            var clientPaymentDetail = _clientPaymentAppService.ClientPaymentDetail(new OneParam { Id = id });
            return View(clientPaymentDetail);
        }
    }
}
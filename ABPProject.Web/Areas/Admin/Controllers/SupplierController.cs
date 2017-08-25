using ABPProject.CommonDto;
using ABPProject.Suppliers;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    public class SupplierController : ABPProjectControllerBase
    {
        private readonly ISupplierAppService _supplierAppService;
        public SupplierController(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _supplierAppService.GetPagedSupplier(pageArg);
            return Json(result);
        }
    }
}
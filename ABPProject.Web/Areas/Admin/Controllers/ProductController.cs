using Abp.Web.Mvc.Authorization;
using ABPProject.Authorization;
using ABPProject.CommonDto;
using ABPProject.Products;
using ABPProject.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Product)]
    public class ProductController : ABPProjectControllerBase
    {
        private readonly IProductAppService _productAppService;
        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        // GET: Admin/Project
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(PageParams pageArg)
        {
            var result = _productAppService.GetPagedProduct(pageArg);
            return Json(result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABPProject.Web.Controllers
{
    public class ProductController : ABPProjectControllerBase
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexMain()
        {
            return View();
        }
    }
}
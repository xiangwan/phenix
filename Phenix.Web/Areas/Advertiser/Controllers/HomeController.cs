using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phenix.Web.Areas.Advertiser.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Advertiser/Home/

        public ActionResult Index()
        {
            return Json("/Advertiser/Home/", JsonRequestBehavior.AllowGet);
        }
    }
}
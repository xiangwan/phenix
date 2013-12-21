using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phenix.Web.Areas.Developer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Json("/Developer/Home/", JsonRequestBehavior.AllowGet);
        }
    }
}
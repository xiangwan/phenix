using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Phenix.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name)
        {
            switch (name)
            {
                case "admin":
                    ResponseCookie("admin", "1", true);
                    return Redirect("/admin/home");
                    break;
                case "developer":
                    ResponseCookie("developer", "2", true);
                    return Redirect("/developer/home");
                    break;
                case "advertiser":
                    ResponseCookie("advertiser", "3", true);
                    return Redirect("/advertiser/home");
                    break;
            }

            ViewBag.Msg = "用户名或密码错误！";
            return View();
        }

        public RedirectToRouteResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private void ResponseCookie(string userName, string userRole, bool isPersistent = false)
        {
            string userdata = userRole;
            int timeout = isPersistent ? 14*24*60 : 3*60;
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(timeout),
                isPersistent, userdata, FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            Response.Cookies.Add(cookie);
        }
    }
}
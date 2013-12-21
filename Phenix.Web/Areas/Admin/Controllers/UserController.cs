using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phenix.Core.Domain;
using Phenix.Core.Repository;
using Phenix.Infrastructure;
using Phenix.Infrastructure.Extensions;

namespace Phenix.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _repo;

        public UserController(IRepository<User> repo)
        {
            _repo = repo;
        }


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(FormCollection form)
        {
            string colkey = form["colkey"];
            string colsinfo = form["colsinfo"];
            int pageIndex = form["page"].ToInt(1);
            int pageSize = form["rp"].ToInt(1);
            var list = _repo.GetPagedList(pageIndex, pageSize);
            var data = FlexiGridDataJson.ConvertFromPagedList(list, colkey, colsinfo.Split(','));
            return Json(data);
        }

        public ActionResult Edit(int? id)
        {
            User user = id.HasValue ? _repo.GetById(id.Value) : new User() {Id = 0, CreateOn = DateTime.Now};
            return View(user);
        }

        [HttpPost]
        public JsonResult Edit(User user)
        {
            var msg = Save(user);
            return Json(msg);
        }

        private JsonReturnMessages Save(User user)
        {
            var msg = new JsonReturnMessages();
            try
            {
                if (user.Id == 0)
                {
                    user.CreateOn = DateTime.Now;
                    user.LastLoginOn = DateTime.Now;
                    user.EMailVerifyOn = DateTime.Now;
                    _repo.Add(user);
                }
                else
                {
                    _repo.Update(user);
                }
                msg.IsSuccess = true;
                msg.Msg = "操作成功";
            }
            catch (Exception e)
            {
                msg.IsSuccess = false;
                msg.Msg = e.Message;
            }
            return msg;
        }
    }
}
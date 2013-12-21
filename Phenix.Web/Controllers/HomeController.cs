using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phenix.Core.Domain;
using Phenix.Core.Repository;
using Phenix.Data;
using Phenix.Infrastructure.Data;

namespace Phenix.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<User> _repository;

        public HomeController(IRepository<User> repository)
        {
            this._repository = repository;
        }

        public ActionResult Index(int page = 1)
        {
            return View(_repository.GetPagedList(page, 10));
        }

        public ActionResult CreateDb()
        {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                for (int i = 0; i < 10; i++)
                {
                    var user = new User()
                    {
                        Name = "用户" + i,
                        PassWord = "PassWord" + i,
                        EMail = "a@b.com",
                        EMailIsValid = false,
                        IsBanned = false,
                        CreateOn = DateTime.Now,
                        LastLoginOn = DateTime.Now,
                        EMailVerifyOn = DateTime.Now
                    };
                    _repository.Add(user);
                }
            }
            return View("Index", _repository.GetPagedList(1, 10));
        }
    }
}
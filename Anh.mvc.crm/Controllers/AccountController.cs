using Anh.mvc.crm.Authentication;
using Anh.mvc.crm.Models;
using Module.BusinessLogic.Shared;
using Module.BusinessLogic.SharedCore;
using Module.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Anh.mvc.crm.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessLogic _businessLogic;

        public AccountController()
        {
            _businessLogic = new BusinessLogic(new UnitOfWork());
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("IsUserLichCongTac", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

    }
}
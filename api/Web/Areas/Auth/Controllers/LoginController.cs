using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Model;
using Utls;
using BLL;

namespace Web.Areas.Auth.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Auth/Login/
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // GET: /Auth/Login/Details/5
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "请输入用户名");
                return View();
            }

            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "请输入密码");
                return View();
            }

            AuthBll bll = new AuthBll();

            sysuser usr = bll.Login(username, password);

            if (usr == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("error", "用户名密码不正确");
                return View();
            }
            else
            {
                base.UserInfo = usr;

                return RedirectToAction("Index", "Login", new { Area = "Auth" });
            }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}

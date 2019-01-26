using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Model;
using BLL;
using Utls;

namespace Web.Areas.Auth.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult ModifyPwd()
        {
            sysuser usr = new sysuser();
            return View(usr);
        }

        //
        // GET: /Auth/Account/
        [HttpPost]
        public ActionResult ModifyPwd(sysuser model)
        {
            if (model.NewPassword == model.Password)
            {
                ModelState.AddModelError("NewPassword", "新密码与旧密码一致，修改失败");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "密码输入不一致");
                return View(model);
            }

            string oldpwd = model.Password;
            string newpwd = model.NewPassword;

            var bll = new UserBll();
            model = bll.GetUser(base.UserInfo.Userid);

            if (model.Password != oldpwd)
            {
                ModelState.AddModelError("Password", "旧密码输入错误");
                return View(model);
            }

            model.Password = newpwd;

            try
            {
                bll.Update<sysuser>(model);
            }
            catch (ValidException ex)
            {
                this.ModelState.AddModelError(ex.Name, ex.Message);
                return View(model);
            }

            return this.RefreshParent();
        }

        //
        // GET: /Auth/Account/Details/5
        [AllowAnonymous]
        public ActionResult Logout()
        {
            UserLoginInfo.Clear();
            return RedirectToAction("Login", "Login", new { Area = "Auth" });
        }

        [AllowAnonymous]
        public ActionResult ErrorA()
        {
            return View();
        }
    }
}

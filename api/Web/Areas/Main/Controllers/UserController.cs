using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Model;
using Utls;
using BLL;

namespace Web.Areas.main.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /main/User/

        public ActionResult Index(string username, string tel, int? pageIndex)
        {
            var bll = new UserBll();
            var users = bll.GetUsers(username, tel, pageIndex.GetValueOrDefault(1));

            //var pids = bll.GetPositions();
            //ViewData["Positionid"] = pids;

            return View(users);
        }

        //
        // GET: /main/User/Create

        public ActionResult Create()
        {
            var model = new sysuser();
            model.Password = "111111";

            var bll = new UserBll();

            Init(bll);

            return View(model);
        }

        private void Init(UserBll bll)
        {
            List<SelectListItem> sts = new List<SelectListItem>();//0正常 1暂停 2删除
            sts.Add(new SelectListItem { Text = "正常", Value = "10", Selected = true });
            sts.Add(new SelectListItem { Text = "删除", Value = "12" });
            //sts.Add(new SelectListItem { Text = "删除", Value = "2" });
            SelectList lst = new SelectList(sts);
            ViewData["Status"] = lst;
        }

        //
        // POST: /main/User/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var model = new sysuser();
            this.TryUpdateModel<sysuser>(model);
            var bll = new UserBll();

            try
            {
                bll.AddUser(model);
            }
            catch (ValidException ex)
            {
                this.ModelState.AddModelError(ex.Name, ex.Message);
                return View(model);
            }

            return this.RefreshParent();
        }

        //
        // GET: /main/User/Edit/5

        public ActionResult Edit(string id)
        {
            var bll = new UserBll();
            var model = bll.GetUser(id);

            Init(bll);

            return View(model);
        }

        //
        // POST: /main/User/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var bll = new UserBll();
            var model = bll.GetUser(id);
            this.TryUpdateModel<sysuser>(model);
        
            try
            {
                bll.EditUser(model);
            }
            catch (ValidException ex)
            {
                this.ModelState.AddModelError(ex.Name, ex.Message);
                Init(bll);

                return View(model);
            }

            return this.RefreshParent();
        }

        //
        // GET: /main/User/Delete/5

        public ActionResult Delete(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(List<string> ids)
        {
            var bll = new UserBll();

            try
            {
                bll.DeleteUser(ids);
            }
            catch (ValidException ex)
            {
                return Alert(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}

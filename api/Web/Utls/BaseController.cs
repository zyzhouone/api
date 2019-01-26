using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using log4net;
using Model;

namespace Web
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public sysuser UserInfo
        {
            get { return UserLoginInfo.Get(); }
            set { UserLoginInfo.Add(value); }
        }

        /// <summary>
        /// AOP拦截，在Action执行后
        /// </summary>
        /// <param name="filterContext">filter context</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var noAuthorizeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false);
            if (noAuthorizeAttributes.Length > 0)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            //没有登录
            sysuser usr = UserInfo;
            if (usr == null)
            {
                filterContext.Result = RedirectToAction("Login", "Login", new { Area = "Auth" });
                return;
            }

            ////页面权限
            ////MenuHepler.List.MenuGroups.AsParallel().ForAll((p) =>
            //foreach (var p in MenuHepler.List.MenuGroups)
            //{
            //    var file = ((filterContext.RequestContext.HttpContext).Request).FilePath;

            //    if (p.MenuArray.Any(a => a.Url.Contains(file) && !a.Permission.Contains("," + usr.Positionid + ",")))
            //    {
            //        filterContext.Result = RedirectToAction("ErrorA", "Account", new { Area = "Auth" });
            //        return;
            //    }
            //}
            ////);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            ILog log = LogManager.GetLogger(filterContext.GetType());
            log.Error(filterContext.Exception);

            base.OnException(filterContext);
        }

        /// <summary>
        /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
        /// </summary>
        /// <returns></returns>
        public ContentResult RefreshParent(string alert = null)
        {
            var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        /// <summary>
        /// 需要刷新浏览器整个页面
        /// </summary>
        /// <returns></returns>
        public ContentResult RefreshPage(string alert = null)
        {
            var script = string.Format("<script>{0};history.go(-1);</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        /// <summary>
        /// 关闭弹出DIV
        /// </summary>
        /// <returns></returns>
        public ContentResult CloseDiv()
        {
            return Content("<script>parent.$.fancybox.close();</script>");
        }

        /// <summary>
        /// 提示框
        /// </summary>
        /// <returns></returns>
        public ContentResult Alert(string msg)
        {
            return Content(string.Format("<script>alert('{0}');</script>", msg));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using log4net;
using Model;

namespace AppAPI
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public tblusers UserInfo
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
            //var noAuthorizeAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false);
            //if (noAuthorizeAttributes.Length > 0)
            //{
            //    base.OnActionExecuting(filterContext);
            //    return;
            //}

            //string token = filterContext.HttpContext.Request.QueryString.Get("token");
            ////return;

            //if (string.IsNullOrEmpty(token))
            //{
            //    filterContext.Result = RedirectToAction("NoAudit", "api");
            //    return;
            //}

            //if (!CheckToken(token))
            //{
            //    filterContext.Result = RedirectToAction("NoAudit", "api");
            //    return;
            //}

            ////没有登录
            //tblusers usr = UserInfo;
            //if (usr == null)
            //{
            //    filterContext.Result = RedirectToAction("Login", "Login", new { Area = "Auth" });
            //    return;
            //}

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

        protected bool CheckToken(string token)
        {
            object usrid = Utls.Caching.Get(token);
            if (usrid == null)
            {
                var u = new BLL.ApiBll().GetUByToken(token);
                if (u == null)
                    return false;

                Utls.Caching.Set(token, u.userid);
                Utls.Caching.Set(u.userid, token);
                return true;
            }

            object tk = Utls.Caching.Get(usrid.ToString());
            if (tk == null)
            {
                var u = new BLL.ApiBll().GetUById(usrid.ToString());
                if (u == null)
                    return false;

                Utls.Caching.Set(token, u.userid);
                Utls.Caching.Set(u.userid, u.DeviceToken);

                tk = u.DeviceToken;
            }

            if (token != tk.ToString())
                return false;
            else
                return true;
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

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            var k = new CustomJsonResult();
            k.Data = data;
            k.ContentType = contentType;
            k.ContentEncoding = contentEncoding;
            k.JsonRequestBehavior = behavior;

            return k;
        }

        protected JsonResult RepReurnOK()
        {
            RepMsg rg = new RepMsg() { code = "0", status = "ok" };

            return Json(rg, "application/json", System.Text.Encoding.Default, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult RepReurnOK(object data)
        {
            RepMsg rg = new RepMsg() { code = "0", status = "ok", data = data };
            return Json(rg, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult RepReurnError(string msg)
        {
            RepMsg rg = new RepMsg() { code = "-1", status = "error", msg = msg };
            return Json(rg, "application/json", System.Text.Encoding.Default, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult RepReurn(string code, string status, string msg, object data)
        {
            RepMsg rg = new RepMsg() { code = code, status = status, msg = msg, data = data };
            return Json(rg, "application/json", System.Text.Encoding.Default, JsonRequestBehavior.AllowGet);
        }
    }

    /// <summary>
    /// 自定义Json视图
    /// </summary>
    public class CustomJsonResult : JsonResult
    {
        /// <summary>
        /// 重写执行视图
        /// </summary>
        /// <param name="context">上下文</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;

            if (string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                string jsonString = getJson(this.Data);
                response.Write(jsonString);
            }
        }

        private string getJson(object rg)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter isoDateTimeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            //设置日期格式
            isoDateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //序列化
            return JsonConvert.SerializeObject(rg, isoDateTimeConverter);
        }
    }
}
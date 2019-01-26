using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //初始化log4net配置
            var config = Server.MapPath("~/Config/log4net.xml");
            FileInfo finfo = new FileInfo(config);
            log4net.Config.XmlConfigurator.Configure(finfo);

            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
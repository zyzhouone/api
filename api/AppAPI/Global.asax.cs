using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppAPI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^1(3|5|8|9)\d{9}$");
            //bool k = regex.IsMatch("12317460712");

               

            //BLL.ApiBll bll = new BLL.ApiBll();
            //bll.ttest();

            //string ll = Utls.AESEncryption.AESEncrypt("6a61b95b-2d5d-4373-abaf-bf4e4c438800", "124", "1234567890123456");
            //string kk = Utls.AESEncryption.AESEncrypt("李kk涛", "124", "1234567890123456");
            
            //初始化log4net配置
            var config = Server.MapPath("~/Config/log4net.xml");
            FileInfo finfo = new FileInfo(config);
            log4net.Config.XmlConfigurator.Configure(finfo);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
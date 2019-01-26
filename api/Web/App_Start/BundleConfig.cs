using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region 登录页面样式
            /***************************登录页面样式**************************/
            StyleBundle lgcss = new StyleBundle("~/assets/lgcss");
            lgcss.Orderer = new AsIsBundleOrderer();
            bundles.Add(lgcss.Include(
                  "~/assets/bootstrap/css/bootstrap.css",
                  "~/assets/css/metro.css",
                  "~/assets/font-awesome/css/font-awesome.css",
                  "~/assets/css/style.css",
                  "~/assets/css/style_responsive.css",
                  "~/assets/css/style_default.css",
                  "~/assets/uniform/css/uniform.default.css",
                  "~/content/styles/admin.main.css"));

            ScriptBundle lgjq = new ScriptBundle("~/assets/lgjq");
            lgjq.Orderer = new AsIsBundleOrderer();
            bundles.Add(lgjq.Include(
                  "~/assets/js/jquery-{version}.js",
                  "~/assets/bootstrap/js/bootstrap.js",
                  "~/assets/js/jquery.blockui.js",
                  "~/assets/js/app.js",
                  "~/content/Scripts/jquery.validate.js",
                  "~/content/Scripts/jquery.validate.unobtrusive.js"));
            #endregion

            #region 内容页面样式
            StyleBundle css = new StyleBundle("~/assets/css");
            css.Orderer = new AsIsBundleOrderer();
            bundles.Add(css.Include(
                    "~/assets/bootstrap/css/bootstrap.min.css",
                    "~/assets/css/metro.css",
                    "~/assets/bootstrap/css/bootstrap-responsive.min.css",
                    "~/assets/font-awesome/css/font-awesome.css",
                    "~/assets/css/style.css",
                    "~/assets/css/style_responsive.css",
                    "~/assets/css/style_default.css",
                    "~/assets/css/style_light.css",
                    "~/assets/fancybox/source/jquery.fancybox.css",
                    "~/assets/uniform/css/uniform.default.css",
                    "~/assets/chosen-bootstrap/chosen/chosen.css",
                    "~/assets/data-tables/DT_bootstrap.css",
                    "~/assets/uniform/css/uniform.default.css",
                    "~/assets/jquery-ui/jquery-ui-1.10.1.custom.min.css",
                    "~/content/styles/admin.main.css",
                    "~/content/styles/jquery.thickbox.css"
                    ));

            StyleBundle css2 = new StyleBundle("~/assets/css2");
            css2.Orderer = new AsIsBundleOrderer();
            bundles.Add(css2.Include(                
                    "~/content/styles/admin.main.css",
                    "~/content/styles/jquery.thickbox.css"
                    ));

            ScriptBundle jq = new ScriptBundle("~/assets/jq");
            jq.Orderer = new AsIsBundleOrderer();
            bundles.Add(jq.Include(
                    "~/assets/js/jquery-{version}.js",
                    "~/assets/breakpoints/breakpoints.js",
                    "~/assets/bootstrap/js/bootstrap.js",
                    "~/assets/js/jquery.blockui.js",
                    "~/assets/js/jquery.cookie.js"
                    ));

            bundles.Add(new ScriptBundle("~/assets/js/ie9").Include(
                    "~/assets/js/excanvas.js",
                    "~/assets/js/respond.js"));

            ScriptBundle js = new ScriptBundle("~/assets/js");
            js.Orderer = new AsIsBundleOrderer();
            bundles.Add(js.Include(
                    //"~/assets/uniform/jquery.uniform.js",
                    "~/assets/data-tables/jquery.dataTables.js",
                    "~/assets/data-tables/DT_bootstrap.js"));
                    //"~/assets/js/app.js"));

             ScriptBundle js2 = new ScriptBundle("~/assets/js2");
            js2.Orderer = new AsIsBundleOrderer();
            bundles.Add(js2.Include(
                    "~/content/scripts/jquery.thickbox.js",
                    "~/assets/jquery-ui/jquery-ui-1.10.4.custom.js",
                    "~/content/scripts/jquery.numeric.js",
                    "~/content/scripts/jquery.ui.datapicker-zh-CN.js",
                    "~/content/scripts/admin.main.js"));
            #endregion

            #region 编辑页面样式
            StyleBundle edcss = new StyleBundle("~/assets/edcss");
            edcss.Orderer = new AsIsBundleOrderer();
            bundles.Add(edcss.Include(
                        "~/assets/bootstrap/css/bootstrap.css",
                      "~/assets/css/metro.css",
                      "~/assets/font-awesome/css/font-awesome.css",
                      "~/assets/css/style.css",
                      "~/assets/jquery-ui/jquery-ui-1.10.1.custom.css",
                      "~/content/styles/admin.main.css"));

            ScriptBundle edjq = new ScriptBundle("~/assets/edjq");
            edjq.Orderer = new AsIsBundleOrderer();
            bundles.Add(edjq.Include(
                      "~/assets/js/jquery-{version}.js",
                      "~/assets/bootstrap/js/bootstrap.js",
                      "~/content/scripts/jquery.validate.js",
                      "~/content/scripts/jquery.validate.unobtrusive.js",
                      "~/content/scripts/jquery.numeric.js",
                      "~/assets/jquery-ui/jquery-ui-1.10.1.custom.js",
                      "~/content/scripts/jquery.ui.datapicker-zh-CN.js",
                      "~/content/scripts/admin.edit.js"));
            #endregion

            //BundleTable.EnableOptimizations = true;
        }
    }
}
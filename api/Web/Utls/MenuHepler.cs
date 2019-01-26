using System.IO;
using System.Web;
using Utls;

namespace Web
{
    public class MenuHepler
    {
        /// <summary>
        /// 菜单配置
        /// </summary>
        public static MenuConfig List
        {
            get
            {
                MenuConfig mc = Caching.Get("pk_menu_config") as MenuConfig;

                if (mc == null)
                {
                    string xml = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Config/MenuConfig.xml"));
                    mc = SerializeHelper.XmlDeserialize<MenuConfig>(xml);

                    Caching.Set("pk_menu_config", mc, 120);
                }

                return mc;
            }
        }
    }
}
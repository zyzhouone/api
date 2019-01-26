using System.Web.Mvc;

namespace Web.Areas.main
{
    public class mainAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "main";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "main_default",
                "main/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

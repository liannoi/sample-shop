using System.Web.Mvc;
using System.Web.Routing;

namespace Shop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "GoodsFind", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}
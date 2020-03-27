using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Shop.Legacy.WebUI.System.Helpers.DependencyInjection;

namespace Shop.Legacy.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Autofac MVC.
            var unused = new ContainerConfig();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
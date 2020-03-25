using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Shop.WebUI.DependencyInjection;

namespace Shop.WebUI
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
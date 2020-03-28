using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Shop.Clients.WebApi.Core.Filters.Mvc;

namespace Shop.Clients.WebApi
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            GlobalFilters.Filters.Add(new ValidateModelAttribute());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
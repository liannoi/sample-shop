using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Shop.WebApi.Identity.Core;
using Shop.WebApi.Identity.Core.Stores;

namespace Shop.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ShopIdentityWebApiContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            // TODO: Refactor.
            // app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) => new RoleManager<AppRole>(new RoleStore<AppRole>(context.Get<ShopIdentityWebApiContext>())));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/AdministratorIdentity/Login")
            });
        }
    }
}
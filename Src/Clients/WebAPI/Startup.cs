using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using Shop.WebApi.Identity;
using Shop.WebApi.Identity.Stores;

namespace Shop.WebApi
{
    // TODO: Try to move Identity/IdentityConfig.cs
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfig = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(app);
            ConfigureWebApi(httpConfig);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        #region Helpers

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ShopIdentityWebApiContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            // TODO: JWT zone.
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        #endregion
    }
}
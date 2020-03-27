using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Shop.WebApi.Identity.Core;
using Shop.WebApi.Identity.Core.Stores;
using Shop.WebApi.Identity.Infrastructure.Providers;

namespace Shop.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);

            var httpConfig = new HttpConfiguration();
            RemoveXmlFormatter(httpConfig);
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

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                // For dev-environment only (on production should be AllowInsecureHttp = false).
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:54351")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] {"Any"},
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider("http://localhost:54351", secret)
                    }
                });
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void RemoveXmlFormatter(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

        #endregion
    }
}
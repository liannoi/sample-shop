﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Shop.Legacy.WebUI.System.Identity.Core.Managers;
using Shop.Legacy.WebUI.System.Identity.Core.Models;
using Shop.Legacy.WebUI.System.Identity.Core.Stores;

namespace Shop.Legacy.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ShopIdentityContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
                new RoleManager<AppRole>(new RoleStore<AppRole>(context.Get<ShopIdentityContext>())));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/AdministratorIdentity/Login")
            });
        }
    }
}
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.WebApi.Identity.Models;

namespace Shop.WebApi.Identity.Stores
{
    public class ShopIdentityWebApiContextInitializer : DropCreateDatabaseIfModelChanges<ShopIdentityWebApiContext>
    {
        protected override void Seed(ShopIdentityWebApiContext context)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new ShopIdentityWebApiContext()));

            var user = new AppUser
            {
                UserName = "SuperPowerUser",
                Email = "taiseer.joudeh@mymail.com",
                EmailConfirmed = true
            };

            manager.Create(user, "MySuperP@ssword!");
        }
    }
}
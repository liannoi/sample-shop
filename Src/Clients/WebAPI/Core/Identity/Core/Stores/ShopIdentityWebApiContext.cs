using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Clients.WebApi.Core.Identity.Core.Models;

namespace Shop.Clients.WebApi.Core.Identity.Core.Stores
{
    public class ShopIdentityWebApiContext : IdentityDbContext<AppUser>
    {
        public ShopIdentityWebApiContext() : base("DefaultConnection", false)
        {
            if (!Database.Exists())
                Database.SetInitializer(new ShopIdentityWebApiContextInitializer());
        }

        public static ShopIdentityWebApiContext Create()
        {
            return new ShopIdentityWebApiContext();
        }
    }
}
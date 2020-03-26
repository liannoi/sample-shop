using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.WebUI.Identity.Models;

namespace Shop.WebUI.Identity.Stores
{
    public class ShopIdentityContext : IdentityDbContext<AppUser>
    {
        public ShopIdentityContext()
        {
            if (!Database.Exists())
                Database.SetInitializer(new ShopIdentityContextInitializer());
        }
    }
}
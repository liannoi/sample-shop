using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.Legacy.WebUI.System.Identity.Core.Models;

namespace Shop.Legacy.WebUI.System.Identity.Core.Stores
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
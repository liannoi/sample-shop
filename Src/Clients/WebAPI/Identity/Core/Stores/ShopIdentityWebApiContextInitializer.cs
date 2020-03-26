using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.WebApi.Identity.Core.Models;

namespace Shop.WebApi.Identity.Core.Stores
{
    public class ShopIdentityWebApiContextInitializer : DropCreateDatabaseIfModelChanges<ShopIdentityWebApiContext>
    {
        #region Helpers

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private async Task SeedAsync(ShopIdentityWebApiContext context)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new ShopIdentityWebApiContext()));

            if (!context.Users.Any(u => u.UserName == "test@gmail.com"))
            {
                var user = new AppUser
                {
                    UserName = "test@gmail.com",
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true
                };
                await manager.CreateAsync(user, "MySuperP@ssword!");
            }
        }

        #endregion

        protected override async void Seed(ShopIdentityWebApiContext context)
        {
            await Task.Factory.StartNew(() => SeedAsync(context));
            base.Seed(context);
        }
    }
}
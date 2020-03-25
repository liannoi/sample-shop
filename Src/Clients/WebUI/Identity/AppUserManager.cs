using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Shop.WebUI.Identity.Models;

namespace Shop.WebUI.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(new UserStore<AppUser>(context.Get<ShopIdentityContext>()));

            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider == null) return manager;

            manager.UserTokenProvider =
                new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            return manager;
        }
    }
}
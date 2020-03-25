using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.WebUI.Identity;
using Shop.WebUI.Identity.Models;
using Shop.WebUI.Identity.ViewModels;

namespace Shop.WebUI.Controllers.Identity
{
    public class UserIdentityController : IdentityController
    {
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var user = new AppUser {UserName = register.Email, Email = register.Email};

            var result = await userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                HttpContext.GetOwinContext().Authentication.SignIn(
                    new AuthenticationProperties {IsPersistent = false},
                    userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
                userManager.AddToRole(userManager.FindByName(register.Email).Id, Consts.UserRoleName);
                return Redirect(Url.Action("Index", "GoodsFind"));
            }

            AddErrors(result);

            return View(register);
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            var user = userManager.Find(login.Email, login.Password);
            if (user != null)
            {
                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties {IsPersistent = false},
                    userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
                return Redirect(Url.Action("Index", "GoodsFind"));
            }

            ModelState.AddModelError("", "Invalid username or password");

            return View(login);
        }
    }
}
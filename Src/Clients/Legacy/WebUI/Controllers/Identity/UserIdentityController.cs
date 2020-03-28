using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.Legacy.WebUI.System;
using Shop.Legacy.WebUI.System.Identity.Core.Managers;
using Shop.Legacy.WebUI.System.Identity.Core.Models;
using Shop.Legacy.WebUI.System.Identity.Infrastructure.Models.Binding;

namespace Shop.Legacy.WebUI.Controllers.Identity
{
    public class UserIdentityController : IdentityController
    {
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            // TODO: Refactor to common.
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var user = new AppUser {UserName = register.Email, Email = register.Email};

            var result = await userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = false},
                    userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
                userManager.AddToRole(userManager.FindByName(register.Email).Id, Consts.UserRoleName);
                return Redirect(Url.Action("Index", "GoodsFind"));
            }

            AddErrors(result);

            return View(register);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            // TODO: Refactor to common.
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            var user = userManager.Find(login.Email, login.Password);
            if (user != null)
            {
                AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = false},
                    userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));
                return Redirect(Url.Action("Index", "GoodsFind"));
            }

            ModelState.AddModelError("", "Invalid username or password");

            return View(login);
        }
    }
}
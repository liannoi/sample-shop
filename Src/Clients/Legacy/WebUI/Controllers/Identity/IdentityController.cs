using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.Legacy.WebUI.Identity.Models;

namespace Shop.Legacy.WebUI.Controllers.Identity
{
    public class IdentityController : Controller
    {
        #region Identity (non actions)

        [HttpGet]
        [AllowAnonymous]
        public RedirectResult CreateRole(string roleName)
        {
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();
            if (!roleManager.RoleExists(roleName))
                roleManager.Create(new AppRole(roleName));
            return Redirect(Url.Action("Index", "GoodsFind"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "GoodsFind");
        }

        #endregion

        #region Helpers

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error);
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index", "Goods");
        }

        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        #endregion
    }
}
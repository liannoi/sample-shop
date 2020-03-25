using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shop.WebUI.Identity.Models;

namespace Shop.WebUI.Controllers.Identity
{
    public class IdentityController : Controller
    {
        #region Identity (non actions)

        [HttpGet]
        public RedirectResult CreateRole(string roleName)
        {
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();
            if (!roleManager.RoleExists(roleName))
                roleManager.Create(new AppRole(roleName));
            return Redirect(Url.Action("Index", "GoodsFind"));
        }

        #endregion

        #region Helpers

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error);
        }

        #endregion
    }
}
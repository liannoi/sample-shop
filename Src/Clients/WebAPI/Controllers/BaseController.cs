using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shop.WebApi.Identity.Core;
using Shop.WebApi.Identity.Infrastructure.Models;

namespace Shop.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private AppUserManager _appUserManager;
        private ModelFactory _modelFactory;

        protected AppUserManager AppUserManager => _appUserManager ??
                                                   (_appUserManager = Request.GetOwinContext()
                                                       .GetUserManager<AppUserManager>());

        protected ModelFactory ModelFactory =>
            _modelFactory ?? (_modelFactory = new ModelFactory(Request, AppUserManager));

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null) return InternalServerError();

            if (result.Succeeded) return null;
            if (result.Errors != null)
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error);

            if (ModelState.IsValid)
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();

            return BadRequest(ModelState);
        }
    }
}
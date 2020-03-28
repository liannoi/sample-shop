using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fluentx.Mvc;
using Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Binding;

namespace Shop.Clients.WebApi.Core.Filters.Mvc
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid) return;

            foreach (var error in (from value in filterContext.Controller.ViewData.ModelState.Values
                    from error in value.Errors
                    select error.ErrorMessage)
                .ToList()) filterContext.Controller.ViewData.ModelState.AddModelError("", error);
            RedirectAndPostActionResult.RedirectAndPost("http://localhost:51480/Home/RegisterUser",
                new Dictionary<string, object>
                {
                    {"bindingModel", filterContext.ActionParameters["bindingModel"] as RegisterBindingModel},
                    {"roleName", "User"}
                });
        }
    }
}
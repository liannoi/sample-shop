using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Infrastructure.Application.Core.Helpers.Tools;
using Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Binding;
using Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Returnable;

namespace Shop.Clients.WebApi.Controllers.Mvc
{
    public class HomeController : Controller
    {
        private readonly IApiTools _apiTools;

        public HomeController()
        {
            // TODO: Dependency injection.
            _apiTools = new ApiTools();
        }

        [System.Web.Mvc.HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ViewResult RegisterAdministrator()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ViewResult RegisterUser()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> RegisterAdministrator([FromBody] RegisterBindingModel bindingModel)
        {
            return await RegisterAsync(bindingModel, "Administrator");
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterBindingModel bindingModel)
        {
            return await RegisterAsync(bindingModel, "User");
        }

        #region Helpers

        private async Task<ActionResult> RegisterAsync([FromBody] RegisterBindingModel bindingModel,
            [FromBody] string roleName)
        {
            bindingModel.RoleName = roleName;
            var result =
                await _apiTools.PostAsync<UserReturnModel>("http://localhost:51480/api/identity/users/create",
                    bindingModel);
            if (result.Id == null) return View(bindingModel);

            // Authentication passed.
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
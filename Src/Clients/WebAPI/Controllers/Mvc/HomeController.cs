using System;
using System.Threading.Tasks;
using System.Web;
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

        [System.Web.Mvc.HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginBindingModel bindingModel)
        {
            var result =
                await _apiTools.PostAsync<UserReturnModel>("http://localhost:51480/api/identity/users/access",
                    bindingModel);

            if (result.Id == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(bindingModel);
            }

            //
            // Authentication is successful.
            //
            // Check that the user has confirmed the mail, if not - urge to
            // confirm. Since without this, we do not pass the test on the
            // issuing component of the JWT token. So far, I set the value to
            // True manually in the database.
            //
            // After that, save the token on the server to the session (in RAM)
            // named "jwt-${userId}". Ideally, saving to light (non-relational
            // DBMS, such as MongoDB) to reduce memory consumption and server
            // load.
            //
            //
            var userId = result.Id;

            var jwt = await _apiTools.PostAsync<JwtReturnModel>(
                "http://localhost:51480/api/identity/users/access/token", new JwtAccessReturnModel
                {
                    ClearPassword = bindingModel.Password,
                    Id = userId
                });

            // BUG: High load.
            Session[$"jwt-{userId}"] = jwt;
            Session[$"jwt-{userId}-user"] =
                await _apiTools.FetchAsync<UserReturnModel>($"http://localhost:51480/api/identity/users/id/{userId}");

            // BUG: Unsafe.
            var myCookie = new HttpCookie("jwt-userId") {Value = userId, Expires = DateTime.MinValue};
            Response.Cookies.Add(myCookie);

            return RedirectToAction(nameof(Index));
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

            // Authentication is successful.
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region User in role

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

        #endregion
    }
}
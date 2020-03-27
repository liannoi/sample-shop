using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Infrastructure.Application.Core.Helpers.Tools;
using Newtonsoft.Json;
using Shop.WebUI.Models.Returnable;
using Shop.WebUI.Models.View;

namespace Shop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiTools _apiTools;
        private readonly CancellationTokenSource _tokenSource;

        public HomeController()
        {
            // TODO: Dependency injection.
            _apiTools = new ApiTools();

            _tokenSource = new CancellationTokenSource();
        }

        public CancellationToken CancellationToken => _tokenSource.Token;

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            IEnumerable<UserReturnModel> result = null;
            try
            {
                // TODO: Address to Consts.cs.
                result = (await _apiTools.FetchAsync<UserReturnModel>("http://localhost:54351/api/identity/users",
                    CancellationToken)).ToList();
            }
            catch (UnauthorizedAccessException e)
            {
                // TODO: To logger, with DI.
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }

            return View(result);
        }

        [HttpGet]
        public ViewResult RegisterAdministrator()
        {
            return View();
        }

        [HttpGet]
        public ViewResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAdministrator(RegisterViewModel viewModel)
        {
            return await Register(viewModel, "Administrator");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterUser(RegisterViewModel viewModel)
        {
            return await Register(viewModel, "User");
        }

        #region Helpers

        private async Task<ActionResult> Register(RegisterViewModel viewModel, string roleName)
        {
            if (!ModelState.IsValid) return View(viewModel);

            viewModel.RoleName = roleName;
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(viewModel)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // TODO: Address to Consts.cs.
            var tmp = await _apiTools.PostAsync<UserReturnModel>("http://localhost:54351/api/identity/users/create",
                byteContent, CancellationToken);

            if (tmp != null) return RedirectToAction(nameof(Index));

            _tokenSource.Cancel();
            return View(viewModel);
        }

        #endregion
    }
}
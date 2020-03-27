using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Infrastructure.Application.Core.Exceptions.Domain;
using Infrastructure.Application.Core.Helpers.Fetching;
using Shop.WebUI.Models.Returnable;

namespace Shop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var fetcher = new ApiFetcher();
            IEnumerable<UserReturnModel> result = null;
            try
            {
                result =
                    (await fetcher.FetchAsync<UserReturnModel>("http://localhost:54351/api/identity/users")).ToList();
            }
            catch (BadStatusCodeException e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }

            return View(result);
        }
    }
}
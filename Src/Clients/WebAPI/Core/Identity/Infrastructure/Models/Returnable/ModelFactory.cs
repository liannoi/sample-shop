using System.Net.Http;
using System.Web.Http.Routing;
using Shop.Clients.WebApi.Core.Identity.Core.Managers;
using Shop.Clients.WebApi.Core.Identity.Core.Models;

namespace Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Returnable
{
    public class ModelFactory
    {
        private readonly AppUserManager _appUserManager;
        private readonly UrlHelper _urlHelper;

        public ModelFactory(HttpRequestMessage request, AppUserManager appUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _appUserManager = appUserManager;
        }

        public UserReturnModel Create(AppUser appUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link(Consts.GetUserByIdActionName, new {id = appUser.Id}),
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Roles = _appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _appUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }
    }
}
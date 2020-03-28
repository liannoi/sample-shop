using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Infrastructure.Application.Core.Helpers.Tools;
using Microsoft.AspNet.Identity;
using Shop.Clients.WebApi.Core.Filters.Api;
using Shop.Clients.WebApi.Core.Identity;
using Shop.Clients.WebApi.Core.Identity.Core.Models;
using Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Binding;
using Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Returnable;

namespace Shop.Clients.WebApi.Controllers.Api
{
    public class IdentityController : BaseController
    {
        private readonly IApiTools _apiTools;

        public IdentityController()
        {
            _apiTools = new ApiTools();
        }

        [HttpGet]
        [Route("api/identity/users")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(AppUserManager.Users.ToList().Select(u => ModelFactory.Create(u)));
        }

        [HttpGet]
        [Route("api/identity/users/id/{id}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUserById(string id)
        {
            var user = await AppUserManager.FindByIdAsync(id);
            return user != null ? (IHttpActionResult) Ok(ModelFactory.Create(user)) : NotFound();
        }

        [HttpGet]
        [Route("api/identity/users/name/{userName}")]
        public async Task<IHttpActionResult> GetUserByName(string userName)
        {
            var user = await AppUserManager.FindByNameAsync(userName);
            return user != null ? (IHttpActionResult) Ok(ModelFactory.Create(user)) : NotFound();
        }

        [HttpPost]
        [Route("api/identity/users/create")]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateUser(RegisterBindingModel bindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new AppUser {Email = bindingModel.Email, UserName = bindingModel.Email};
            var result = await AppUserManager.CreateAsync(user, bindingModel.Password);
            return !result.Succeeded
                ? GetErrorResult(result)
                : Created(new Uri(Url.Link(Consts.GetUserByIdActionName, new {id = user.Id})),
                    ModelFactory.Create(user));
        }

        [HttpPost]
        [Route("api/identity/users/access")]
        [ValidateModel]
        public async Task<IHttpActionResult> AccessUser(LoginBindingModel bindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await AppUserManager.FindAsync(bindingModel.Email, bindingModel.Password);
            return result == null
                ? GetErrorResult(new IdentityResult("Invalid username or password."))
                : Ok(ModelFactory.Create(result));
        }

        [HttpPost]
        [Route("api/identity/users/access/token")]
        public async Task<IHttpActionResult> GetAccessUserToken([FromBody] JwtAccessReturnModel access)
        {
            return Ok(await _apiTools.RequestToken<JwtReturnModel>("http://localhost:51480/oauth/token",
                new Dictionary<string, string>
                {
                    {"username", (await AppUserManager.FindByIdAsync(access.Id)).Email},
                    {"password", access.ClearPassword},
                    {"grant_type", "password"}
                }));
        }

        //[Authorize]
        //[Route("api/identity/users/delete/{id}")]
        //[HttpDelete]
        //public async Task<IHttpActionResult> DeleteUser(string id)
        //{
        //    var appUser = await AppUserManager.FindByIdAsync(id);
        //    if (appUser == null) return NotFound();
        //    var result = await AppUserManager.DeleteAsync(appUser);
        //    return !result.Succeeded ? GetErrorResult(result) : Ok(ModelFactory.Create(appUser));
        //}
    }
}
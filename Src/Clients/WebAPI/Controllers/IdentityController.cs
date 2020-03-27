using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Shop.WebApi.Identity;
using Shop.WebApi.Identity.Core.Models;
using Shop.WebApi.Identity.Infrastructure.Models.Binding;

namespace Shop.WebApi.Controllers
{
    public class IdentityController : BaseController
    {
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

        [Authorize]
        [Route("api/identity/users/delete/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var appUser = await AppUserManager.FindByIdAsync(id);
            if (appUser == null) return NotFound();
            var result = await AppUserManager.DeleteAsync(appUser);
            return !result.Succeeded ? GetErrorResult(result) : Ok(ModelFactory.Create(appUser));
        }
    }
}
using System.Net;
using System.Web.Mvc;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.WebUI.Entities;
using Shop.WebUI.ViewModels.Users;

namespace Shop.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly IBusinessService<PhotoDto> _photoRepository;

        public UsersController(IBusinessService<GoodDto> goodRepository, IBusinessService<PhotoDto> photoRepository)
        {
            _goodRepository = goodRepository;
            _photoRepository = photoRepository;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(new GoodViewViewModel(_goodRepository, _photoRepository));
        }

        /// <summary>
        ///     Add to cart.
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpStatusCodeResult Add(int goodId)
        {
            SaveCartToSession(goodId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Cart display.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Show()
        {
            return View(new CartViewModel(_goodRepository, ReadCartFromSession()));
        }

        /// <summary>
        ///     Updating the number of products taken.
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpStatusCodeResult Update(int goodId, int count)
        {
            var sessionCart = ReadCartFromSession();
            sessionCart.Update(goodId, count);
            UpdateCartSession(sessionCart);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        ///     Remove product from cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpStatusCodeResult Delete(int id)
        {
            var sessionCart = ReadCartFromSession();
            sessionCart.Delete(id);
            UpdateCartSession(sessionCart);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #region Helpers

        private void UpdateCartSession(UserCart sessionCart)
        {
            Session[Consts.SessionCartName] = sessionCart;
        }

        private void SaveCartToSession(int goodId)
        {
            var sessionCart = ReadCartFromSession();
            sessionCart.AddOrUpdate(goodId);
            UpdateCartSession(sessionCart);
        }

        private UserCart ReadCartFromSession()
        {
            return Session[Consts.SessionCartName] as UserCart ?? new UserCart();
        }

        #endregion
    }
}
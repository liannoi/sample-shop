using System.Net;
using System.Web.Mvc;
using Infrastructure.Application.Core.Services.Business;
using Shop.Application.Entities;
using Shop.Legacy.WebUI.System;
using Shop.Legacy.WebUI.System.Models.Entities;
using Shop.Legacy.WebUI.System.Models.View.Users;

namespace Shop.Legacy.WebUI.Controllers.Sides.Administrator
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

        // TODO: This view, javascript to separate file.
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public HttpStatusCodeResult Add(int goodId)
        {
            SaveCartToSession(goodId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // TODO: This view, javascript - ajax.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Cart()
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
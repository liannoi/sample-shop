using System.Collections.Generic;
using System.Linq;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.Application.Storage.Good;
using Shop.WebUI.Entities;

namespace Shop.WebUI.ViewModels.Users
{
    public class CartViewModel
    {
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly UserCart _userCart;

        public CartViewModel(IBusinessService<GoodDto> goodRepository, UserCart userCart)
        {
            _goodRepository = goodRepository;
            _userCart = userCart;
            InitializeFriendlyCarts();
        }

        public List<CartExtension> FriendlyCarts { get; private set; }

        private void InitializeFriendlyCarts()
        {
            FriendlyCarts = new List<CartExtension>();
            foreach (var cartExt in from cart in _userCart.Carts
                let good = _goodRepository.SelectSafe(cart.GoodId)
                select new CartExtension
                {
                    Cart = _userCart,
                    GoodName = good.GoodName,
                    Price = good.Price,
                    GoodSum = good.Price * cart.GoodCount
                })
                FriendlyCarts.Add(cartExt);
        }
    }
}
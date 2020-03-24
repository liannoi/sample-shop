using System.Collections.Generic;

namespace Shop.WebUI.Entities
{
    public class UserCart
    {
        public UserCart()
        {
            Carts = new List<Cart>();
        }

        public List<Cart> Carts { get; }
    }
}
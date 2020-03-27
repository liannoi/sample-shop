using System.Collections.Generic;

namespace Shop.Legacy.WebUI.System.Models.Entities
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
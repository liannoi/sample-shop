namespace Shop.Legacy.WebUI.System.Models.Entities
{
    public class CartExtension
    {
        public UserCart Cart { get; set; }
        public string GoodName { get; set; }
        public decimal Price { get; set; }
        public decimal GoodSum { get; set; }
    }
}
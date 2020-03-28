namespace Shop.Legacy.WebUI.System.Models.Entities
{
    public static class UserCartExtensions
    {
        public static Cart Exists(this UserCart self, int goodId)
        {
            return self.Carts.Find(e => e.GoodId == goodId);
        }

        public static void AddOrUpdate(this UserCart self, int goodId)
        {
            var tmp = Exists(self, goodId);
            if (tmp != null)
                tmp.GoodCount += 1;
            else
                self.Carts.Add(new Cart {GoodId = goodId, GoodCount = 1});
        }

        public static void Delete(this UserCart self, int goodId)
        {
            self.Carts.Remove(Exists(self, goodId));
        }

        public static void Update(this UserCart self, int goodId, decimal count)
        {
            self.Exists(goodId).GoodCount = count;
        }
    }
}
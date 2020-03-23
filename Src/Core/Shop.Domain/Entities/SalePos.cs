namespace Shop.Domain.Entities
{
    public class SalePos
    {
        public int SalePosId { get; set; }

        public int SaleId { get; set; }

        public int GoodId { get; set; }

        public int GoodCount { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Good Good { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
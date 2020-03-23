namespace Shop.Application.Entities
{
    public class SalePosDto
    {
        public int SalePosId { get; set; }

        public int SaleId { get; set; }

        public int GoodId { get; set; }

        public int GoodCount { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
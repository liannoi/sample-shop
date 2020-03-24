namespace Shop.Domain.ValueObjects
{
    public interface IGoodView<TKey>
    {
        TKey GoodId { get; set; }
        string GoodName { get; set; }
        decimal Price { get; set; }
        string PhotoPath { get; set; }
    }
}
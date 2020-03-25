namespace Shop.Domain.ValueObjects
{
    public interface IGoodView<TKey, TPrice>
    {
        TKey GoodId { get; set; }
        string GoodName { get; set; }
        TPrice Price { get; set; }
        string PhotoPath { get; set; }
    }
}
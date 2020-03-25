namespace Shop.Domain.ValueObjects
{
    public interface IGoodInfo<TKey, TPrice>
    {
        TKey GoodId { get; set; }
        TPrice Price { get; set; }
        string GoodName { get; set; }
        string PhotoPath { get; set; }
    }
}
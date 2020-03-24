namespace Shop.Domain.ValueObjects
{
    public interface IGoodInfo<TKey>
    {
        TKey GoodId { get; set; }
        string GoodName { get; set; }
        string PhotoPath { get; set; }
    }
}
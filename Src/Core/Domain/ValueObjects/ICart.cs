namespace Shop.Domain.ValueObjects
{
    public interface ICart<TKey, TCount>
    {
        TKey GoodId { get; set; }
        TCount GoodCount { get; set; }
    }
}
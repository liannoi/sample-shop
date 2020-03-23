using Shop.Domain.ValueObjects;

namespace Shop.UnitOfWork.ConsoleTests.Entities
{
    /// <summary>
    ///     A business entity that relates to the business logic of this particular
    ///     (and only this) client.
    /// </summary>
    public class Cart : ICart<int, int>
    {
        public int GoodId { get; set; }
        public int GoodCount { get; set; }
    }
}
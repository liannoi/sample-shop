using System.Collections.Generic;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Sale.UnitOfWork.Handler
{
    public interface ISaleUnitOfWorkHandler<TCart>
    {
        void MakePurchase(SaleDto sale, IEnumerable<TCart> carts);
    }
}
using System.Transactions;
using Infrastructure.Application.Core.Services.Business;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Sale.UnitOfWork
{
    public interface ISaleUnitOfWork : IUnitOfWork
    {
        TransactionScope Transactions { get; set; }
        IBusinessService<SaleDto> SaleRepository { get; }
        IBusinessService<SalePosDto> SalePosRepository { get; }
        IBusinessService<GoodDto> GoodRepository { get; }
    }
}
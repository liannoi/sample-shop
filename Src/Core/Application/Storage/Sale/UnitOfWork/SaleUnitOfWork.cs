using System;
using System.Transactions;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Sale.UnitOfWork
{
    public class SaleUnitOfWork : ISaleUnitOfWork
    {
        private bool _disposed;

        public SaleUnitOfWork(IBusinessService<SaleDto> saleRepository, IBusinessService<SalePosDto> salePosRepository,
            IBusinessService<GoodDto> goodRepository)
        {
            SaleRepository = saleRepository;
            SalePosRepository = salePosRepository;
            GoodRepository = goodRepository;
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            Transactions.Complete();
        }

        public TransactionScope Transactions { get; set; }
        public IBusinessService<SaleDto> SaleRepository { get; }
        public IBusinessService<SalePosDto> SalePosRepository { get; }
        public IBusinessService<GoodDto> GoodRepository { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) Transactions.Dispose();

            _disposed = true;
        }
    }
}
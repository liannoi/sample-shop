using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Shop.Application.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Storage.Sale.UnitOfWork.Handler
{
    public class SaleUnitOfWorkHandler : ISaleUnitOfWorkHandler<ICart<int, int>>
    {
        private readonly ISaleUnitOfWork _unitOfWork;

        public SaleUnitOfWorkHandler(ISaleUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void MakePurchase(SaleDto sale, IEnumerable<ICart<int, int>> carts)
        {
            using (_unitOfWork.Transactions = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var item = _unitOfWork.SaleRepository.Add(sale);

                    foreach (var cart in carts)
                    {
                        Expression<Func<GoodDto, bool>> tmp = e => e.GoodId == cart.GoodId;

                        var good = _unitOfWork.GoodRepository.Find(tmp).FirstOrDefault() ?? throw new Exception();
                        good.GoodCount -= cart.GoodCount;

                        _unitOfWork.GoodRepository.Update(tmp, good);

                        _unitOfWork.SalePosRepository.Add(new SalePosDto
                        {
                            SaleId = item.SaleId,
                            GoodId = cart.GoodId,
                            GoodCount = cart.GoodCount,
                            UnitPrice = good.Price
                        });
                    }

                    _unitOfWork.Save();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error...");
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
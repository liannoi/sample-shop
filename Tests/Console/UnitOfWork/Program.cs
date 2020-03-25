using System;
using System.Collections.Generic;
using Autofac;
using Shop.Application.Entities;
using Shop.Application.Storage.Sale.UnitOfWork.Handler;
using Shop.Domain.ValueObjects;
using Shop.UnitOfWork.ConsoleTests.DependencyInjection;
using Shop.UnitOfWork.ConsoleTests.Entities;

namespace Shop.UnitOfWork.ConsoleTests
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Setup.
            var containerConfig = new ContainerConfig();

            var unitOfWorkHandler = containerConfig.Container.Resolve<ISaleUnitOfWorkHandler<ICart<int, int>>>();
            unitOfWorkHandler.MakePurchase(new SaleDto {HireDate = DateTime.Now, UserId = 1}, new List<Cart>
            {
                new Cart {GoodId = 1, GoodCount = 1}
            });
        }
    }
}
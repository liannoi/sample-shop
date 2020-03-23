using System.Data.Entity;
using Autofac;
using Shop.Application.Storage.Sale.UnitOfWork.Handler;
using Shop.Domain.ValueObjects;
using Shop.Persistence;
using Shop.UnitOfWork.ConsoleTests.Entities;

namespace Shop.UnitOfWork.ConsoleTests.DependencyInjection
{
    public class ConsoleDependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShopContext>().As<DbContext>();
            builder.RegisterType<Cart>().As<ICart<int, int>>();
        }
    }
}
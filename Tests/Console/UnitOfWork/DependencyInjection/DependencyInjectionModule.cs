using Autofac;
using Shop.Domain.ValueObjects;
using Shop.UnitOfWork.ConsoleTests.Entities;

namespace Shop.UnitOfWork.ConsoleTests.DependencyInjection
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Cart>().As<ICart<int, int>>();
        }
    }
}
using System.Data.Entity;
using Autofac;
using Infrastructure.Application.Core.DependencyInjection;
using Shop.Application;
using Shop.Persistence;

namespace Shop.Autofac.ConsoleTests.DependencyInjection
{
    public sealed class ConsoleContainerConfig : ContainerConfig<DependencyInjectionModule>
    {
        public ConsoleContainerConfig()
        {
            Container = Build();
        }

        public override IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ShopContext>().As<DbContext>();
            builder.RegisterModule<DependencyInjectionModule>();
            return builder.Build();
        }
    }
}
using Autofac;
using Infrastructure.Application.Core.DependencyInjection;
using Shop.Application;

namespace Shop.Autofac.ConsoleTests.DependencyInjection
{
    public sealed class ContainerConfig : ContainerConfig<DependencyInjectionModule>
    {
        public ContainerConfig()
        {
            Container = Build();
        }

        public override IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<Persistence.DependencyInjectionModule>();
            builder.RegisterModule<DependencyInjectionModule>();
            return builder.Build();
        }
    }
}
using Autofac;
using Infrastructure.Application.Core.DependencyInjection;
using Shop.Application;

namespace Shop.UnitOfWork.ConsoleTests.DependencyInjection
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
            builder.RegisterModule<ConsoleDependencyInjectionModule>();
            builder.RegisterModule<DependencyInjectionModule>();
            return builder.Build();
        }
    }
}
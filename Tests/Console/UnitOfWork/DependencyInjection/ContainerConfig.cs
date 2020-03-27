using Autofac;
using Infrastructure.Application.Core.Helpers.DependencyInjection;

namespace Shop.UnitOfWork.ConsoleTests.DependencyInjection
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
            builder.RegisterModule<Persistence.System.Helpers.DependencyInjection.DependencyInjectionModule>();
            builder.RegisterModule<Application.Helpers.DependencyInjection.DependencyInjectionModule>();
            builder.RegisterModule<DependencyInjectionModule>();
            return builder.Build();
        }
    }
}
using Autofac;
using Autofac.Core;

namespace Infrastructure.Application.Core.DependencyInjection
{
    public class ContainerConfig<TModule> : IContainerConfig where TModule : IModule, new()
    {
        public IContainer Container { get; protected set; }

        public virtual IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TModule>();
            return builder.Build();
        }
    }
}
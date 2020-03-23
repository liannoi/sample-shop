using Autofac;

namespace Infrastructure.Application.Core.DependencyInjection
{
    public interface IContainerConfig
    {
        IContainer Container { get; }
        IContainer Build();
    }
}
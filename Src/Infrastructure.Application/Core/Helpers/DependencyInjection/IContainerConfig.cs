using Autofac;

namespace Infrastructure.Application.Core.Helpers.DependencyInjection
{
    public interface IContainerConfig
    {
        IContainer Container { get; }
        IContainer Build();
    }
}
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Infrastructure.Application.Core.Helpers.DependencyInjection;
using Shop.Application.Helpers.DependencyInjection;

namespace Shop.Legacy.WebUI.System.Helpers.DependencyInjection
{
    public sealed class ContainerConfig : ContainerConfig<DependencyInjectionModule>
    {
        public ContainerConfig()
        {
            // Not used.
            Container = Build();
        }

        public override IContainer Build()
        {
            // Initialization.
            var containerBuilder = new ContainerBuilder();

            // Registration.
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            containerBuilder.RegisterModule<Persistence.System.Helpers.DependencyInjection.DependencyInjectionModule>();
            containerBuilder.RegisterModule<DependencyInjectionModule>();

            // Setup.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.Build()));

            // Not used.
            return null;
        }
    }
}
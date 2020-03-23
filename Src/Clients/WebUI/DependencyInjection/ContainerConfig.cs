using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Infrastructure.Application.Core.DependencyInjection;

namespace Shop.WebUI.DependencyInjection
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
            containerBuilder.RegisterModule<Persistence.DependencyInjectionModule>();
            containerBuilder.RegisterModule<Application.DependencyInjectionModule>();
            containerBuilder.RegisterModule<DependencyInjectionModule>();

            // Setup.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.Build()));

            // Not used.
            return null;
        }
    }
}
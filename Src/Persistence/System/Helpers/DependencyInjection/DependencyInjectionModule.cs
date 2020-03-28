using System.Data.Entity;
using Autofac;

namespace Shop.Persistence.System.Helpers.DependencyInjection
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShopContext>().As<DbContext>();
        }
    }
}
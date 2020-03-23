using System.Data.Entity;
using Autofac;
using Shop.Persistence;

namespace Shop.WebUI.DependencyInjection
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShopContext>().As<DbContext>();
        }
    }
}
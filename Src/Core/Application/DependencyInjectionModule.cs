using Autofac;
using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;
using Shop.Application.Storage.Category;
using Shop.Application.Storage.Good;
using Shop.Application.Storage.Manufacturer;
using Shop.Application.Storage.Photo;
using Shop.Application.Storage.Sale;
using Shop.Application.Storage.SalePos;
using Shop.Domain.Entities;

namespace Shop.Application
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterStorage<CategoryService, Category, CategoryRepository, CategoryDto>(builder);
            RegisterStorage<GoodService, Good, GoodRepository, GoodDto>(builder);
            RegisterStorage<ManufacturerService, Manufacturer, ManufacturerRepository, ManufacturerDto>(builder);
            RegisterStorage<PhotoService, Photo, PhotoRepository, PhotoDto>(builder);
            RegisterStorage<SaleService, Sale, SaleRepository, SaleDto>(builder);
            RegisterStorage<SalePosService, SalePos, SalePosRepository, SalePosDto>(builder);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void RegisterStorage<TStorage, TEntity, TRepository, TBEntity>(ContainerBuilder builder)
            where TEntity : class, new() where TBEntity : class, new()
        {
            builder.RegisterType<TStorage>().As<IDataService<TEntity>>();
            builder.RegisterType<TRepository>().As<IBusinessService<TBEntity>>();
        }
    }
}
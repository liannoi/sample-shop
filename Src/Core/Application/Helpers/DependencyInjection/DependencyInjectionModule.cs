using Autofac;
using Infrastructure.Application.Core.Services.Business;
using Infrastructure.Application.Core.Services.Data;
using Shop.Application.Entities;
using Shop.Application.Storage.Category;
using Shop.Application.Storage.Good;
using Shop.Application.Storage.Manufacturer;
using Shop.Application.Storage.Photo;
using Shop.Application.Storage.Sale;
using Shop.Application.Storage.Sale.UnitOfWork;
using Shop.Application.Storage.Sale.UnitOfWork.Handler;
using Shop.Application.Storage.SalePos;
using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Helpers.DependencyInjection
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Storage.
            RegisterStorage<CategoryService, Category, CategoryRepository, CategoryDto>(builder);
            RegisterStorage<GoodService, Good, GoodRepository, GoodDto>(builder);
            RegisterStorage<ManufacturerService, Manufacturer, ManufacturerRepository, ManufacturerDto>(builder);
            RegisterStorage<PhotoService, Photo, PhotoRepository, PhotoDto>(builder);
            RegisterStorage<SaleService, Sale, SaleRepository, SaleDto>(builder);
            RegisterStorage<SalePosService, SalePos, SalePosRepository, SalePosDto>(builder);

            // Sales transactions.
            builder.RegisterType<SaleUnitOfWork>().As<ISaleUnitOfWork>();
            builder.RegisterType<SaleUnitOfWorkHandler>().As<ISaleUnitOfWorkHandler<ICart<int, int>>>();
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
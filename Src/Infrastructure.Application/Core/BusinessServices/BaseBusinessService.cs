using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Infrastructure.Application.Core.BusinessServices.Helpers;
using Infrastructure.Application.Core.DataServices;

namespace Infrastructure.Application.Core.BusinessServices
{
    public abstract class BaseBusinessService<TEntity, TBEntity> : IBusinessService<TBEntity>
        where TBEntity : class, new() where TEntity : class, new()
    {
        private readonly IDataService<TEntity> _dataService;
        protected IMapper Mapper;

        protected BaseBusinessService(IDataService<TEntity> dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<TBEntity> Select()
        {
            return _dataService.Select().Entities<TEntity, TBEntity>(Mapper);
        }

        public IEnumerable<TBEntity> Find(Expression<Func<TBEntity, bool>> expression)
        {
            return _dataService.Find(Mapper.Map<Expression<Func<TEntity, bool>>>(expression))
                .Entities<TEntity, TBEntity>(Mapper);
        }

        public TBEntity Add(TBEntity entity)
        {
            var result = _dataService.Add(entity.Entity<TEntity, TBEntity>(Mapper));
            _dataService.Commit();
            return result.Entity<TEntity, TBEntity>(Mapper);
        }

        public TBEntity Update(Expression<Func<TBEntity, bool>> expressionToFindOld, TBEntity entity)
        {
            var result = _dataService.Update(Mapper.Map<Expression<Func<TEntity, bool>>>(expressionToFindOld),
                entity.Entity<TEntity, TBEntity>(Mapper));
            _dataService.Commit();
            return result.Entity<TEntity, TBEntity>(Mapper);
        }

        public TBEntity Remove(TBEntity entity)
        {
            var result = _dataService.Remove(entity.Entity<TEntity, TBEntity>(Mapper));
            _dataService.Commit();
            return result.Entity<TEntity, TBEntity>(Mapper);
        }

        protected virtual IMapper InitializeMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<TEntity, TBEntity>();
                cfg.CreateMap<TBEntity, TEntity>();
            }).CreateMapper();
        }
    }
}
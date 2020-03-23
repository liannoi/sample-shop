using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Application.Core.DataServices
{
    public interface IDataService<TEntity> where TEntity : class, new()
    {
        IEnumerable<TEntity> Select();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        TEntity Add(TEntity entity);
        TEntity Update(Expression<Func<TEntity, bool>> expressionToFindOld, TEntity entity);
        TEntity Remove(TEntity entity);
        int Commit();
    }
}
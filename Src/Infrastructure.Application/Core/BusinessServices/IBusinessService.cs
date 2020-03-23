using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Application.Core.BusinessServices
{
    public interface IBusinessService<TBEntity> where TBEntity : class, new()
    {
        IEnumerable<TBEntity> Select();
        IEnumerable<TBEntity> Find(Expression<Func<TBEntity, bool>> expression);
        TBEntity Add(TBEntity entity);
        TBEntity Update(Expression<Func<TBEntity, bool>> expressionToFindOld, TBEntity entity);
        TBEntity Remove(TBEntity entity);
    }
}
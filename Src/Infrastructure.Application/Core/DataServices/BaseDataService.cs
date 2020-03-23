using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Application.Core.DataServices
{
    public abstract class BaseDataService<TEntity> : IDataService<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _context;
        private readonly IDbSet<TEntity> _entities;

        protected BaseDataService(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        protected BaseDataService(DbContext context, IDbSet<TEntity> entities) : this(context)
        {
            _entities = entities;
        }

        public IEnumerable<TEntity> Select()
        {
            return _entities;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public TEntity Add(TEntity entity)
        {
            return _entities.Add(entity);
        }

        public TEntity Update(Expression<Func<TEntity, bool>> expressionToFindOld, TEntity entity)
        {
            var fined = Find(expressionToFindOld).FirstOrDefault();
            _context.Entry(fined).CurrentValues.SetValues(entity);
            return fined;
        }

        public TEntity Remove(TEntity entity)
        {
            return _entities.Remove(entity);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}
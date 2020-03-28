using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Infrastructure.Application.Core.Helpers.Extensions
{
    public static class Businessable
    {
        public static IEnumerable<TBEntity> Entities<TEntity, TBEntity>(this IEnumerable<TEntity> self, IMapper mapper)
            where TBEntity : class, new() where TEntity : class, new()
        {
            return self.Select(mapper.Map<TBEntity>);
        }

        public static TBEntity Entity<TEntity, TBEntity>(this TEntity self, IMapper mapper)
            where TBEntity : class, new() where TEntity : class, new()
        {
            return mapper.Map<TBEntity>(self);
        }

        public static TEntity Entity<TEntity, TBEntity>(this TBEntity self, IMapper mapper)
            where TBEntity : class, new() where TEntity : class, new()
        {
            return mapper.Map<TEntity>(self);
        }
    }
}
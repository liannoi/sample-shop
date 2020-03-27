using System.Data.Entity;
using Infrastructure.Application.Core.Services.Data;

namespace Shop.Application.Storage.Good
{
    public class GoodService : BaseDataService<Domain.Entities.Good>
    {
        public GoodService(DbContext context) : base(context)
        {
        }

        public GoodService(DbContext context, IDbSet<Domain.Entities.Good> entities) : base(context, entities)
        {
        }
    }
}
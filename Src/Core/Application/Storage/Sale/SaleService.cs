using System.Data.Entity;
using Infrastructure.Application.Core.Services.Data;

namespace Shop.Application.Storage.Sale
{
    public class SaleService : BaseDataService<Domain.Entities.Sale>
    {
        public SaleService(DbContext context) : base(context)
        {
        }

        public SaleService(DbContext context, IDbSet<Domain.Entities.Sale> entities) : base(context, entities)
        {
        }
    }
}
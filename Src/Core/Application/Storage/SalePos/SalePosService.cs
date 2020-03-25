using System.Data.Entity;
using Infrastructure.Application.Core.DataServices;

namespace Shop.Application.Storage.SalePos
{
    public class SalePosService : BaseDataService<Domain.Entities.SalePos>
    {
        public SalePosService(DbContext context) : base(context)
        {
        }

        public SalePosService(DbContext context, IDbSet<Domain.Entities.SalePos> entities) : base(context, entities)
        {
        }
    }
}
using System.Data.Entity;
using Infrastructure.Application.Core.DataServices;

namespace Shop.Application.Storage.Manufacturer
{
    public class ManufacturerService : BaseDataService<Domain.Entities.Manufacturer>
    {
        public ManufacturerService(DbContext context) : base(context)
        {
        }

        public ManufacturerService(DbContext context, IDbSet<Domain.Entities.Manufacturer> entities) : base(context,
            entities)
        {
        }
    }
}
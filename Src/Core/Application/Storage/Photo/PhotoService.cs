using System.Data.Entity;
using Infrastructure.Application.Core.Services.Data;

namespace Shop.Application.Storage.Photo
{
    public class PhotoService : BaseDataService<Domain.Entities.Photo>
    {
        public PhotoService(DbContext context) : base(context)
        {
        }

        public PhotoService(DbContext context, IDbSet<Domain.Entities.Photo> entities) : base(context, entities)
        {
        }
    }
}
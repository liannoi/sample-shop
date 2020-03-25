using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Photo
{
    public sealed class PhotoRepository : BaseBusinessService<Domain.Entities.Photo, PhotoDto>
    {
        public PhotoRepository(IDataService<Domain.Entities.Photo> dataService) : base(dataService)
        {
            Mapper = InitializeMapper();
        }
    }
}
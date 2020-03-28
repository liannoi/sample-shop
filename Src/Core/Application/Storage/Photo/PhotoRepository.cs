using Infrastructure.Application.Core.Services.Business;
using Infrastructure.Application.Core.Services.Data;
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
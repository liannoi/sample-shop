using Infrastructure.Application.Core.Services.Business;
using Infrastructure.Application.Core.Services.Data;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Manufacturer
{
    public sealed class ManufacturerRepository : BaseBusinessService<Domain.Entities.Manufacturer, ManufacturerDto>
    {
        public ManufacturerRepository(IDataService<Domain.Entities.Manufacturer> dataService) : base(dataService)
        {
            Mapper = InitializeMapper();
        }
    }
}
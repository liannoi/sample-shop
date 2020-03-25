using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
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
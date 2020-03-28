using Infrastructure.Application.Core.Services.Business;
using Infrastructure.Application.Core.Services.Data;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Sale
{
    public sealed class SaleRepository : BaseBusinessService<Domain.Entities.Sale, SaleDto>
    {
        public SaleRepository(IDataService<Domain.Entities.Sale> dataService) : base(dataService)
        {
            Mapper = InitializeMapper();
        }
    }
}
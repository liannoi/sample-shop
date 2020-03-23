using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Sale
{
    public class SaleRepository : BaseBusinessService<Domain.Entities.Sale, SaleDto>
    {
        public SaleRepository(IDataService<Domain.Entities.Sale> dataService) : base(dataService)
        {
        }
    }
}
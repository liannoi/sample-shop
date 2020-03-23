using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.SalePos
{
    public class SalePosRepository : BaseBusinessService<Domain.Entities.SalePos, SalePosDto>
    {
        public SalePosRepository(IDataService<Domain.Entities.SalePos> dataService) : base(dataService)
        {
        }
    }
}
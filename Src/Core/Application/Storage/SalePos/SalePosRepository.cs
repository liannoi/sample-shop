using Infrastructure.Application.Core.Services.Business;
using Infrastructure.Application.Core.Services.Data;
using Shop.Application.Entities;

namespace Shop.Application.Storage.SalePos
{
    public sealed class SalePosRepository : BaseBusinessService<Domain.Entities.SalePos, SalePosDto>
    {
        public SalePosRepository(IDataService<Domain.Entities.SalePos> dataService) : base(dataService)
        {
            Mapper = InitializeMapper();
        }
    }
}
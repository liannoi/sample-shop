using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Good
{
    public class GoodRepository : BaseBusinessService<Domain.Entities.Good, GoodDto>
    {
        public GoodRepository(IDataService<Domain.Entities.Good> dataService) : base(dataService)
        {
        }
    }
}
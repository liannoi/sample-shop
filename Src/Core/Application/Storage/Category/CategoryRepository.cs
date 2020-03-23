using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Category
{
    public class CategoryRepository : BaseBusinessService<Domain.Entities.Category, CategoryDto>
    {
        public CategoryRepository(IDataService<Domain.Entities.Category> dataService) : base(dataService)
        {
        }
    }
}
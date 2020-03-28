using Infrastructure.Application.Core.Services.Business;
using Infrastructure.Application.Core.Services.Data;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Category
{
    public sealed class CategoryRepository : BaseBusinessService<Domain.Entities.Category, CategoryDto>
    {
        public CategoryRepository(IDataService<Domain.Entities.Category> dataService) : base(dataService)
        {
            Mapper = InitializeMapper();
        }
    }
}
using System.Data.Entity;
using Infrastructure.Application.Core.Services.Data;

namespace Shop.Application.Storage.Category
{
    public class CategoryService : BaseDataService<Domain.Entities.Category>
    {
        public CategoryService(DbContext context) : base(context)
        {
        }

        public CategoryService(DbContext context, IDbSet<Domain.Entities.Category> entities) : base(context, entities)
        {
        }
    }
}
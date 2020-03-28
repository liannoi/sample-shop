using System.Linq;
using Infrastructure.Application.Core.Services.Business;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Category
{
    public static class CategoryStorage
    {
        public static CategoryDto SelectSafe(this IBusinessService<CategoryDto> self, int id)
        {
            return self.Find(e => e.CategoryId == id).FirstOrDefault();
        }
    }
}
using System.Linq;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Good
{
    public static class GoodStorage
    {
        public static GoodDto SelectSafe(this IBusinessService<GoodDto> self, int id)
        {
            return self.Find(e => e.GoodId == id).FirstOrDefault();
        }
    }
}
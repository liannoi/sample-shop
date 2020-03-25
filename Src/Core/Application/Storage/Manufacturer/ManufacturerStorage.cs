using System.Linq;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Manufacturer
{
    public static class ManufacturerStorage
    {
        public static ManufacturerDto SelectSafe(this IBusinessService<ManufacturerDto> self, int id)
        {
            return self.Find(e => e.ManufacturerId == id).FirstOrDefault();
        }
    }
}
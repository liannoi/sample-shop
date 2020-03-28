using System.Linq;
using Infrastructure.Application.Core.Services.Business;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Photo
{
    public static class PhotoStorage
    {
        public static PhotoDto SelectSafe(this IBusinessService<PhotoDto> self, int id)
        {
            return self.Find(e => e.PhotoId == id).FirstOrDefault();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.WebUI.ClientApp;

namespace Shop.WebUI.ViewModels
{
    public class GoodsFindByFilterViewModel
    {
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly GoodsFindViewModel _goodsFindViewModel;
        private readonly IBusinessService<PhotoDto> _photoRepository;

        public GoodsFindByFilterViewModel(IBusinessService<PhotoDto> photoRepository,
            IBusinessService<GoodDto> goodRepository, GoodsFindViewModel goodsFindViewModel)
        {
            _photoRepository = photoRepository;
            _goodRepository = goodRepository;
            _goodsFindViewModel = goodsFindViewModel;

            // TODO: Refactor.
            var collection = _goodRepository.Find(_goodsFindViewModel.Predicate);
            ListGoodInfos = new List<GoodInfo>();
            foreach (var goodDto in collection)
            {
                var photo = _photoRepository.Find(p => p.GoodId == goodDto.GoodId).FirstOrDefault();
                var goodInfo = new GoodInfo
                {
                    GoodId = goodDto.GoodId,
                    GoodName = goodDto.GoodName,
                    PhotoPath = photo == null ? Consts.NotPhotoPath : photo.PhotoPath
                };
                ListGoodInfos.Add(goodInfo);
            }
        }

        public List<GoodInfo> ListGoodInfos { get; }
    }
}
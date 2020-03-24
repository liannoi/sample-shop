using System.Collections.Generic;
using System.Linq;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.WebUI.ViewModels.GoodsFind
{
    public class ByFilterViewModel
    {
        private readonly BaseViewModel _baseViewModel;
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly IBusinessService<PhotoDto> _photoRepository;

        public ByFilterViewModel(IBusinessService<PhotoDto> photoRepository,
            IBusinessService<GoodDto> goodRepository, BaseViewModel baseViewModel)
        {
            _photoRepository = photoRepository;
            _goodRepository = goodRepository;
            _baseViewModel = baseViewModel;
            InitializeGoodInfos();
        }

        public List<GoodInfo> ListGoodInfos { get; private set; }

        private void InitializeGoodInfos()
        {
            var collection = _goodRepository.Find(_baseViewModel.Predicate);
            ListGoodInfos = new List<GoodInfo>();
            foreach (var goodDto in collection)
            {
                var photo = _photoRepository.Find(p => p.GoodId == goodDto.GoodId).FirstOrDefault();
                var goodInfo = new GoodInfo
                {
                    GoodId = goodDto.GoodId,
                    GoodName = goodDto.GoodName,
                    PhotoPath = photo == null ? ClientApp.Consts.NotPhotoPath : photo.PhotoPath
                };
                ListGoodInfos.Add(goodInfo);
            }
        }

        public class GoodInfo : IGoodInfo<int>
        {
            public int GoodId { get; set; }
            public string GoodName { get; set; }
            public string PhotoPath { get; set; }
        }
    }
}
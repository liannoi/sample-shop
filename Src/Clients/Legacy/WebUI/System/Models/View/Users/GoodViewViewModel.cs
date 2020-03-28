using System.Collections.Generic;
using System.Linq;
using Infrastructure.Application.Core.Services.Business;
using Shop.Application.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Legacy.WebUI.System.Models.View.Users
{
    public class GoodViewViewModel
    {
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly IBusinessService<PhotoDto> _photoRepository;

        public GoodViewViewModel(IBusinessService<GoodDto> goodRepository,
            IBusinessService<PhotoDto> photoRepository)
        {
            _goodRepository = goodRepository;
            _photoRepository = photoRepository;
            InitializeGoodViews();
        }

        // ReSharper disable once CollectionNeverQueried.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public List<GoodView> GoodViews { get; private set; }

        private void InitializeGoodViews()
        {
            GoodViews = new List<GoodView>();
            var goods = _goodRepository.Select();
            foreach (var good in goods)
            {
                var photo = _photoRepository.Find(e => e.GoodId == good.GoodId).FirstOrDefault();
                GoodViews.Add(new GoodView
                {
                    GoodId = good.GoodId,
                    GoodName = good.GoodName,
                    Price = good.Price,
                    PhotoPath = photo == null ? ClientApp.Consts.NotPhotoPath : photo.PhotoPath
                });
            }
        }

        public class GoodView : IGoodView<int, decimal>
        {
            public int GoodId { get; set; }
            public string GoodName { get; set; }
            public decimal Price { get; set; }
            public string PhotoPath { get; set; }
        }
    }
}
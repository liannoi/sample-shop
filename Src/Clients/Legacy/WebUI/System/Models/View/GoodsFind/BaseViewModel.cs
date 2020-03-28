using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Application.Core.Services.Business;
using LinqKit;
using Shop.Application.Entities;

namespace Shop.Legacy.WebUI.System.Models.View.GoodsFind
{
    [Serializable]
    public class BaseViewModel
    {
        private readonly IBusinessService<CategoryDto> _categoryRepository;
        private readonly IBusinessService<ManufacturerDto> _manufacturerRepository;

        public BaseViewModel()
        {
        }

        public BaseViewModel(IBusinessService<CategoryDto> categoryRepository,
            IBusinessService<ManufacturerDto> manufacturerRepository)
        {
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;

            ManufacturerChecks = _manufacturerRepository.Select().Select(c => new ManufacturerCheck
                {ManufacturerId = c.ManufacturerId, ManufacturerName = c.ManufacturerName}).ToList();

            CategoryChecks = _categoryRepository.Select().Select(c => new CategoryCheck
                {CategoryId = c.CategoryId, CategoryName = c.CategoryName}).ToList();
        }

        public string GoodName { get; set; }
        public List<CategoryCheck> CategoryChecks { get; set; }
        public List<ManufacturerCheck> ManufacturerChecks { get; set; }

        public Expression<Func<GoodDto, bool>> Predicate
        {
            get
            {
                var predicate = PredicateBuilder.New<GoodDto>(true);

                if (!string.IsNullOrEmpty(GoodName)) predicate = predicate.And(g => g.GoodName.Contains(GoodName));

                if (!CategoryChecks.Select(c => c.IsCheck).Any()) return predicate;
                {
                    var predicateCategory = PredicateBuilder.New<GoodDto>(true);
                    predicateCategory = CategoryChecks.Where(item => item.IsCheck).Aggregate(predicateCategory,
                        (current, item) => current.Or(c => c.CategoryId == item.CategoryId));

                    predicate = predicate.And(predicateCategory);
                }

                if (!ManufacturerChecks.Select(c => c.IsCheck).Any()) return predicate;
                {
                    var predicateManufacturer = PredicateBuilder.New<GoodDto>(true);
                    predicateManufacturer = ManufacturerChecks.Where(item => item.IsCheck)
                        .Aggregate(predicateManufacturer,
                            (current, item) => current.Or(c => c.ManufacturerId == item.ManufacturerId));

                    predicate = predicate.And(predicateManufacturer);
                }

                return predicate;
            }
        }

        public class CategoryCheck
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public bool IsCheck { get; set; }
        }

        public class ManufacturerCheck
        {
            public int ManufacturerId { get; set; }
            public string ManufacturerName { get; set; }
            public bool IsCheck { get; set; }
        }
    }
}
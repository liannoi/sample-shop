using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Infrastructure.Application.Core.BusinessServices;
using Infrastructure.Application.Core.DataServices;
using Shop.Application.Entities;

namespace Shop.Application.Storage.Good
{
    public sealed class GoodRepository : BaseBusinessService<Domain.Entities.Good, GoodDto>
    {
        public GoodRepository(IDataService<Domain.Entities.Good> dataService) : base(dataService)
        {
            Mapper = InitializeMapper();
        }

        protected override IMapper InitializeMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.CreateMap<Domain.Entities.Good, GoodDto>()
                    .ForMember(nameof(GoodDto.CategoryName), opt => opt.MapFrom(g => g.Category.CategoryName))
                    .ForMember(nameof(GoodDto.ManufacturerName),
                        opt => opt.MapFrom(g => g.Manufacturer.ManufacturerName));
                cfg.CreateMap<GoodDto, Domain.Entities.Good>();
            }).CreateMapper();
        }
    }
}
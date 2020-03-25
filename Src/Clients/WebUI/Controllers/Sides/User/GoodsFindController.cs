using System.Net;
using System.Web.Mvc;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.WebUI.ViewModels.GoodsFind;

namespace Shop.WebUI.Controllers.Sides.User
{
    public class GoodsFindController : Controller
    {
        private readonly IBusinessService<CategoryDto> _categoryRepository;
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly IBusinessService<ManufacturerDto> _manufacturerRepository;
        private readonly IBusinessService<PhotoDto> _photoRepository;

        public GoodsFindController(IBusinessService<CategoryDto> categoryRepository,
            IBusinessService<GoodDto> goodRepository, IBusinessService<ManufacturerDto> manufacturerRepository,
            IBusinessService<PhotoDto> photoRepository)
        {
            _categoryRepository = categoryRepository;
            _goodRepository = goodRepository;
            _manufacturerRepository = manufacturerRepository;
            _photoRepository = photoRepository;
        }

        // TODO: This view, javascript to separate file.
        [HttpGet]
        public ViewResult Index()
        {
            return View(new BaseViewModel(_categoryRepository, _manufacturerRepository));
        }

        [HttpPost]
        public HttpStatusCodeResult Index(BaseViewModel viewModel)
        {
            TempData[Consts.GoodsFindBaseViewModelNameInTempData] = viewModel;
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        [ActionName("_GoodByFilter")]
        public PartialViewResult GoodByFilter()
        {
            return PartialView(new ByFilterViewModel(_photoRepository, _goodRepository,
                TempData[Consts.GoodsFindBaseViewModelNameInTempData] != null
                    ? TempData[Consts.GoodsFindBaseViewModelNameInTempData] as BaseViewModel
                    : new BaseViewModel()));
        }
    }
}
using System.Web.Mvc;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.WebUI.ViewModels;

namespace Shop.WebUI.Controllers
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

        [HttpGet]
        public ActionResult Index()
        {
            return View(new GoodsFindViewModel(_categoryRepository, _manufacturerRepository));
        }

        [HttpPost]
        public JsonResult Index(GoodsFindViewModel viewModel)
        {
            // TODO: Make to Consts.cs.
            TempData["data"] = viewModel;

            // TODO: Status code, not JSON.
            return Json("OK");
        }

        [HttpGet]
        [ActionName("_GoodByFilter")]
        public PartialViewResult GoodByFilter()
        {
            GoodsFindViewModel filter = null;
            if (TempData["data"] != null) filter = TempData["data"] as GoodsFindViewModel;
            var model = new GoodsFindByFilterViewModel(_photoRepository, _goodRepository,
                filter ?? new GoodsFindViewModel());
            return PartialView(model);
        }
    }
}
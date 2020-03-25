using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.Application.Storage.Good;
using Shop.WebUI.Controllers.Helpers;

namespace Shop.WebUI.Controllers.Sides.Administrator
{
    public class GoodsController : Controller
    {
        private readonly IBusinessService<CategoryDto> _categoryRepository;
        private readonly IControllerHelper _controllerHelper;
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly IBusinessService<ManufacturerDto> _manufacturerRepository;

        public GoodsController(IBusinessService<CategoryDto> categoryRepository,
            IBusinessService<GoodDto> goodRepository, IBusinessService<ManufacturerDto> manufacturerRepository)
        {
            _controllerHelper = new ControllerHelper(new ControllerHelper.ControllerViewBagHelper(this));
            _categoryRepository = categoryRepository;
            _goodRepository = goodRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(_goodRepository.Select());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Update(int id)
        {
            var model = id == 0 ? new GoodDto() : _goodRepository.SelectSafe(id);

            // TODO: Encapsulate in ViewModel.
            ViewBag.ManufacturerId = new SelectList(new List<ManufacturerDto>
                {
                    new ManufacturerDto
                    {
                        ManufacturerId = 0, ManufacturerName = "(Nope)"
                    }
                }.Union(_manufacturerRepository.Select()), nameof(ManufacturerDto.ManufacturerId),
                nameof(ManufacturerDto.ManufacturerName), model.ManufacturerId);
            ViewBag.CategoryId = new SelectList(new List<CategoryDto>
                {
                    new CategoryDto
                    {
                        CategoryId = 0, CategoryName = "(Nope)"
                    }
                }.Union(_categoryRepository.Select()), nameof(CategoryDto.CategoryId), nameof(CategoryDto.CategoryName),
                model.CategoryId);

            if (id == 0)
                _controllerHelper.PrepareViewBagsForCreate("Create new good");
            else
                _controllerHelper.PrepareViewBagsForUpdate("Update this good");

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult Update(GoodDto good)
        {
            _controllerHelper.CheckModelState();

            if (good.GoodId == 0)
                _goodRepository.Add(good);
            else
                _goodRepository.Update(e => e.GoodId == good.GoodId, good);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public HttpStatusCodeResult Delete(int id)
        {
            try
            {
                _goodRepository.Remove(_goodRepository.SelectSafe(id));
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }
    }
}
using System;
using System.Net;
using System.Web.Mvc;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.Application.Storage.Category;
using Shop.WebUI.Controllers.Helpers;

namespace Shop.WebUI.Controllers.Sides.Administrator
{
    public class CategoriesController : Controller
    {
        private readonly IBusinessService<CategoryDto> _categoryRepository;
        private readonly IControllerHelper _controllerHelper;

        public CategoriesController(IBusinessService<CategoryDto> categoryRepository)
        {
            _controllerHelper = new ControllerHelper(new ControllerHelper.ControllerViewBagHelper(this));
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(_categoryRepository.Select());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Update(int id)
        {
            if (id == 0)
            {
                _controllerHelper.PrepareViewBagsForCreate("Create new category");
                return View(new CategoryDto());
            }

            _controllerHelper.PrepareViewBagsForUpdate("Update this category");
            return View(_categoryRepository.SelectSafe(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public RedirectToRouteResult Update(CategoryDto category)
        {
            _controllerHelper.CheckModelState();

            if (category.CategoryId == 0)
                _categoryRepository.Add(category);
            else
                _categoryRepository.Update(e => e.CategoryId == category.CategoryId, category);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public HttpStatusCodeResult Delete(int id)
        {
            try
            {
                _categoryRepository.Remove(_categoryRepository.SelectSafe(id));
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }
    }
}
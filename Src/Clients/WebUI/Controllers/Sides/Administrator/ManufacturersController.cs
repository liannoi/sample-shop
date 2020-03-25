using System;
using System.Net;
using System.Web.Mvc;
using Infrastructure.Application.Core.BusinessServices;
using Shop.Application.Entities;
using Shop.Application.Storage.Manufacturer;
using Shop.WebUI.Controllers.Helpers;

namespace Shop.WebUI.Controllers.Sides.Administrator
{
    public class ManufacturersController : Controller
    {
        private readonly IControllerHelper _controllerHelper;
        private readonly IBusinessService<ManufacturerDto> _manufacturerRepository;

        public ManufacturersController(IBusinessService<ManufacturerDto> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _controllerHelper = new ControllerHelper(new ControllerHelper.ControllerViewBagHelper(this));
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_manufacturerRepository.Select());
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            if (id == 0)
            {
                _controllerHelper.PrepareViewBagsForCreate("Create new manufacturer");
                return View(new ManufacturerDto());
            }

            _controllerHelper.PrepareViewBagsForUpdate("Update this manufacturer");
            return View(_manufacturerRepository.SelectSafe(id));
        }

        [HttpPost]
        public RedirectToRouteResult Update(ManufacturerDto manufacturer)
        {
            _controllerHelper.CheckModelState();

            if (manufacturer.ManufacturerId == 0)
                _manufacturerRepository.Add(manufacturer);
            else
                _manufacturerRepository.Update(e => e.ManufacturerId == manufacturer.ManufacturerId, manufacturer);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public HttpStatusCodeResult Delete(int id)
        {
            try
            {
                _manufacturerRepository.Remove(_manufacturerRepository.SelectSafe(id));
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }
    }
}
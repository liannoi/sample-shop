﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Application.Core.Services.Business;
using Shop.Application.Entities;
using Shop.Application.Storage.Good;
using Shop.Application.Storage.Photo;
using Shop.Legacy.WebUI.ClientApp;

namespace Shop.Legacy.WebUI.Controllers.Sides.Administrator
{
    public class PhotosController : Controller
    {
        private readonly IBusinessService<GoodDto> _goodRepository;
        private readonly IBusinessService<PhotoDto> _photoRepository;

        public PhotosController(IBusinessService<GoodDto> goodRepository, IBusinessService<PhotoDto> photoRepository)
        {
            _goodRepository = goodRepository;
            _photoRepository = photoRepository;
        }

        // TODO: This view, javascript to separate file.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Index(int id)
        {
            // TODO: Encapsulate in ViewModel.
            ViewBag.Good = _goodRepository.SelectSafe(id);

            return View(_photoRepository.Find(e => e.GoodId == id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ViewResult Upload(int id)
        {
            return View(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult Upload(IEnumerable<HttpPostedFileBase> fileBases)
        {
            if (fileBases == null) throw new ArgumentNullException(nameof(fileBases));

            // TODO: Encapsulate in ViewModel.
            var id = Convert.ToInt32(Request.Params["id"]);
            foreach (var fileBase in fileBases)
            {
                var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(Path.GetFileName(fileBase.FileName))}";
                fileBase.SaveAs(Path.Combine(
                    $"{AppDomain.CurrentDomain.BaseDirectory}{Consts.GoodsPhotosDirectory}", newFileName));
                _photoRepository.Add(new PhotoDto
                {
                    GoodId = id,
                    PhotoPath = $"{Consts.GoodsPhotosDirectory}{newFileName}"
                });
            }

            return RedirectToAction(nameof(Index), new {id});
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public HttpStatusCodeResult Delete(int id)
        {
            try
            {
                _photoRepository.Remove(_photoRepository.SelectSafe(id));
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }
    }
}
﻿using System.Web.Mvc;

namespace Shop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
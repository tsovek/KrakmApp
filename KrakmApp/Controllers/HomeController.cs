﻿using Microsoft.AspNetCore.Mvc;

namespace KrakmApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

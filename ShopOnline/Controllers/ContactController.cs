﻿using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

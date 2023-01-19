﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaLibrary.Controllers
{
    public class LoggedUserController : Controller
    {
        [Authorize(Roles ="Admin, User")]
        public IActionResult MainPage()
        {
            return View();
        }
    }
}

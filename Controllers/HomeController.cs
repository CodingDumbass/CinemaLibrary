using CinemaLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace CinemaLibrary.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<IdentityUser> SignInManager;
        private UserManager<IdentityUser> UserManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewUser()
        {
            if (SignInManager.IsSignedIn(User))
                return RedirectToAction("MainPage", "LoggedUser");
            else
                return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
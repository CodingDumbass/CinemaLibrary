using CinemaLibrary.Areas.Identity.Pages.Account;
using CinemaLibrary.Core;
using CinemaLibrary.Infrastructure.Models;
using CinemaLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

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

        public async Task<IActionResult> Index()
        {
            if (SignInManager.IsSignedIn(User))
            {
                if (User.IsInRole("User"))
                    return RedirectToAction("MainPage", "LoggedUser");

                else if (User.IsInRole("Admin"))
                    return RedirectToAction("AdminPage", "Admin");
                else
                {
                    var user = await UserManager.GetUserAsync(User);

                    var claims = await UserManager.GetClaimsAsync(user);
                    // Check if the user already has the role claim
                    var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                    if (roleClaim == null)
                        // Add the role claim to the user
                        await UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));

                    //Just in case the wrong claim was used
                    else
                        // Update the user's role claim
                        await UserManager.ReplaceClaimAsync(user, roleClaim, new Claim(ClaimTypes.Role, "User"));

                    return RedirectToAction("Index");
                }
            }
            else
                return View();
        }
        public IActionResult NewUser()
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
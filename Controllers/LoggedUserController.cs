using Microsoft.AspNetCore.Mvc;

namespace CinemaLibrary.Controllers
{
    public class LoggedUserController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }
    }
}

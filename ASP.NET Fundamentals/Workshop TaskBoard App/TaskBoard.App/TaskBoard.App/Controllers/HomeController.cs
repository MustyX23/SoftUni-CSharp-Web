using Microsoft.AspNetCore.Mvc;

namespace TaskBoard.App.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
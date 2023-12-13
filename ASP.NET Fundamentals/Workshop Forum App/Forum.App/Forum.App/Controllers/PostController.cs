using Microsoft.AspNetCore.Mvc;

namespace Forum.App.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

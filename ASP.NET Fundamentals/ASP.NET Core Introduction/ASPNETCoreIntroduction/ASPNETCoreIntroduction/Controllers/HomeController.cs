namespace ASPNETCoreIntroduction.Controllers
{
    using ASPNETCoreIntroduction.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Every method that returns IActionResult is Action
        //Every action corresponds to route
        public IActionResult Index()
        {
            //View Data -> Dictionary
            ViewData["MyData"] = "I am inserting data from home controller in view!";
            ViewBag.Result = "It works!";
            //View Bag -> object
            return View();
        }

        [Route("/Show")]
        public IActionResult Show(int id)
        {
            return this.Json("Custom Routing!");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
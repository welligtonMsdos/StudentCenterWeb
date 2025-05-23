using Microsoft.AspNetCore.Mvc;
using StudentCenterWeb.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace StudentCenterWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;           
        }

        public IActionResult Index()
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

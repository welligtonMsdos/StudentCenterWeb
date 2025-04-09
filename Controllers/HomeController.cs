using Microsoft.AspNetCore.Mvc;
using StudentCenterWeb.Models;
using System.Diagnostics;

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
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            };

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IldlbGxpZ3RvbkFuZHJlU2lsdmFAZ21haWwuY29tIiwiaWQiOiI2N2Y0MmI0YTQwNmYzZjQ3MWFjM2ViY2YiLCJuYmYiOjE3NDQxNjkwMjMsImV4cCI6MTc0NDE3NjIyMywiaWF0IjoxNzQ0MTY5MDIzLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDQ4In0.XpO3D7zMsk0EWWe7NHN7sOmAXidfdI5wY3mtDVTmUtc";

            Response.Cookies.Append("Token", token, cookieOptions);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

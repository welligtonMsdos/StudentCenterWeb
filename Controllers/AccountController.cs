using Microsoft.AspNetCore.Mvc;
using StudentCenterWeb.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace StudentCenterWeb.Controllers;

public class AccountController : Controller
{
    private readonly IStudentCenterAuthService _service;

    public AccountController(IStudentCenterAuthService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(string email, string password)
    {
        var result = await _service.UserLogin(email, password);

        if (result == null || result.success == false) return Unauthorized();

        var cookieOptions = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        };

        var token = result.message;

        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(token);

        var claims = jwtToken.Claims.ToList();       

        Response.Cookies.Append("Token", token, cookieOptions);

        Response.Cookies.Append("userId", claims[1].ToString(), cookieOptions);

        return RedirectToAction("Index", "Home");       
    }
}

using Microsoft.AspNetCore.Mvc;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Models;
using System.Diagnostics;

namespace StudentCenterWeb.Controllers;

public class StudentCenterBaseController : Controller
{
    private readonly IStudentCenterService _service;

    public StudentCenterBaseController(IStudentCenterService service)
    {
        _service = service;
    }

    public async Task<IActionResult> GetAll()
    {
        try
        {
            var studentCenterBase = await _service.GetAllStudentCenterBase();

            return View(studentCenterBase);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

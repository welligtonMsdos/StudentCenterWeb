using Microsoft.AspNetCore.Mvc;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Models;
using System.Diagnostics;

namespace StudentCenterWeb.Controllers;

public class StudentCenterController : Controller
{
    private readonly IStudentCenterService _service;

    public StudentCenterController(IStudentCenterService service)
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

    public async Task<IActionResult> Solicitation(string studentId)
    {
        try
        {
            var id = Util.EncoderHelper.DecodeId(studentId);

            var studentCenterBase = await _service.GetByIdStudentCenterBase(id);

            var mySolicitation = await _service.GetByStudentId(2025);

            if (studentCenterBase != null && !string.IsNullOrEmpty(studentCenterBase.Page))
            {
                return View(studentCenterBase.Page, mySolicitation);
            }

            return View();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSolicitationsByStatus(string status)
    {
        var statusId = 0;

        if (status.Equals("PENDENTE")) statusId = 1;
        else if (status.Equals("CONCLUÍDO")) statusId = 2;
        else if (status.Equals("NEGADO")) statusId = 3;

        var solicitations = await _service.GetSolicitationsByStatusAndStudentId(statusId, 2025);

        return PartialView("~/Views/StudentCenter/PartialViews/_CardSolicitation.cshtml", solicitations);
    }

    public string EncodeId(int id)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(id.ToString());
        return Convert.ToBase64String(bytes);
    }

    public int DecodeId(string encodedId)
    {
        var bytes = Convert.FromBase64String(encodedId);
        return int.Parse(System.Text.Encoding.UTF8.GetString(bytes));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

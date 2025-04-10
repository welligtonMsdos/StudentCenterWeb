using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using StudentCenterWeb.DTOs;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Models;
using System.Diagnostics;

namespace StudentCenterWeb.Controllers;

public class StudentCenterController : Controller
{
    private readonly IStudentCenterService _service;
    private readonly IHttpContextAccessor _accessor;
    private readonly string _userId;

    public StudentCenterController(IStudentCenterService service, IHttpContextAccessor accessor)
    {
        _service = service;

        _accessor = accessor;

        _userId = _accessor.HttpContext.Request.Cookies["userId"];                 
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {   
            var studentCenterBase = await _service.GetAllStudentCenterBase() ?? new List<StudentCenterBaseDto>();

            return View(studentCenterBase);
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Solicitation(string Id)
    {
        try
        {
            var id = Util.EncoderHelper.DecodeId(Id);

            var studentCenterBase = await _service.GetByIdStudentCenterBase(id);           

            if (studentCenterBase != null && !string.IsNullOrEmpty(studentCenterBase.Page))
            {
                if(studentCenterBase.Id == 8)
                {
                    var mySolicitation = await _service.GetByStudentId(_userId) ?? new List<SolicitationDto>();

                    return View(studentCenterBase.Page, mySolicitation);
                }
                else if(studentCenterBase.Id == 7)
                {
                    ViewBag.RequestType = await _service.GetAllRequestType() ?? new List<RequestTypeDto>();                    

                    return View(studentCenterBase.Page);
                }
            }

            return View();
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSolicitationsByStatus(string status)
    {
        var statusId = 0;

        if (status.Equals("Pendente")) statusId = 1;
        else if (status.Equals("Concluído")) statusId = 2;
        else if (status.Equals("Negado")) statusId = 3;       

        var solicitations = await _service.GetSolicitationsByStatusAndStudentId(statusId, _userId);

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

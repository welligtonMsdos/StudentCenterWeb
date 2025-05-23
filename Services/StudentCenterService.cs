﻿using StudentCenterWeb.DTOs;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Util;

namespace StudentCenterWeb.Services;

public class StudentCenterService : IStudentCenterService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";
    private const string STUDENT_CENTER_BASE = "StudentCenterBase";
    private const string SOLICITATION = "Solicitation";
    private const string REQUEST_TYPE = "RequestType";
    private string token;
    private readonly IHttpContextAccessor _accessor;

    public StudentCenterService(HttpClient cliente, IHttpContextAccessor accessor)
    {
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));

        _accessor = accessor;

        token = _accessor.HttpContext.Request.Cookies["Token"];

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<ICollection<RequestTypeDto>> GetAllRequestType()
    {
        var response = await _client.GetAsync(BASE_PATH + REQUEST_TYPE + "/GetAll");

        return await response.ReadContentAs<List<RequestTypeDto>>();
    }

    public async Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase()
    {
        var response = await _client.GetAsync(BASE_PATH + STUDENT_CENTER_BASE);

        return await response.ReadContentAs<List<StudentCenterBaseDto>>();
    }

    public async Task<StudentCenterBaseDto> GetByIdStudentCenterBase(int id)
    {
        var response = await _client.GetAsync(BASE_PATH + STUDENT_CENTER_BASE + "/" + id);

        return await response.ReadContentAs<StudentCenterBaseDto>();
    }

    public async Task<ICollection<SolicitationDto>> GetByStudentId(string studentId)
    {
        var endPoint = string.Format("/GetByStudentId?studentId={0}", studentId);

        var response = await _client.GetAsync(BASE_PATH + SOLICITATION + endPoint);

        return await response.ReadContentAs<ICollection<SolicitationDto>>();
    }

    public async Task<ICollection<SolicitationDto>> GetSolicitationsByStatusAndStudentId(int statusId, string studentId)
    {
        var endPoint = string.Format("/GetByStatusId?statusId={0}&studentId={1}", statusId, studentId);

        var response = await _client.GetAsync(BASE_PATH + SOLICITATION + endPoint);

        return await response.ReadContentAs<ICollection<SolicitationDto>>();
    }

    public async Task<ResponseDto> SaveSolicitation(SolicitationCreateDto solicitationCreateDto)
    {       
        var response = await _client.PostAsJson(BASE_PATH + SOLICITATION, solicitationCreateDto);

        return await response.ReadContentAs<ResponseDto>();
    }
}

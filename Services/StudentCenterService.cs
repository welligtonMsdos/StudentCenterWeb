using StudentCenterWeb.DTOs;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Util;

namespace StudentCenterWeb.Services;

public class StudentCenterService : IStudentCenterService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";
    private const string STUDENT_CENTER_BASE = "StudentCenterBase";

    public StudentCenterService(HttpClient cliente)
    {
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));
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
}

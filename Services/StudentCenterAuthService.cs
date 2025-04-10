using StudentCenterWeb.DTOs;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Util;

namespace StudentCenterWeb.Services;

public class StudentCenterAuthService : IStudentCenterAuthService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";
    private const string LOGIN = "Login";
   
    public StudentCenterAuthService(HttpClient cliente)
    {
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));
    }

    public async Task<UserDataLoginDto> UserLogin(string Email, string PassWord)
    {
        var user = new UserLoginDto(Email, PassWord);

        var response = await _client.PostAsJson(BASE_PATH + LOGIN, user);       

        return await response.ReadContentAs<UserDataLoginDto>();
    }
}

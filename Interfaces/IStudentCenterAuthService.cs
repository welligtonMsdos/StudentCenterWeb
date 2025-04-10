using StudentCenterWeb.DTOs;

namespace StudentCenterWeb.Interfaces;

public interface IStudentCenterAuthService
{
    Task<UserDataLoginDto> UserLogin(string Email, string PassWord);
}

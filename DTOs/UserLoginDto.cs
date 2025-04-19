namespace StudentCenterWeb.DTOs;

public class UserLoginDto
{
    public string Email { get; set; }
    public string PassWord { get; set; }

    public UserLoginDto()
    {
    }

    public UserLoginDto(string email, string passWord)
    {
        Email = email;
        PassWord = passWord;
    }
}

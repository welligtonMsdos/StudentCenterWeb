namespace StudentCenterWeb.DTOs;

public class UserDataLoginDto
{
    public bool success { get; set; }
    public string message { get; set; }
    public DataResponse Data { get; set; }
}

public class DataResponse 
{
    public string Result { get; set; }
}

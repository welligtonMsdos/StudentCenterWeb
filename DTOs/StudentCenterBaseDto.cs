namespace StudentCenterWeb.DTOs;

public class StudentCenterBaseDto
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string Page { get; set; }
}

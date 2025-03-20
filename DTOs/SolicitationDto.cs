namespace StudentCenterWeb.DTOs;

public class SolicitationDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Description { get; set; }
    public string DescriptionStatus { get; set; }
    public string DescriptionRequestType { get; set; }
}

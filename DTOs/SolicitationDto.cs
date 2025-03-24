namespace StudentCenterWeb.DTOs;

public class SolicitationDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public required string Description { get; set; }
    public required string DescriptionStatus { get; set; }
    public required string DescriptionRequestType { get; set; }
}

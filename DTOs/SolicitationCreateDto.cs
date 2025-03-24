namespace StudentCenterWeb.DTOs;

public record SolicitationCreateDto
{   
    public int StudentId { get; init; }
    public string Description { get; init; }
    public int StatusId { get; init; }
    public int RequestTypeId { get; init; }

    public SolicitationCreateDto(int studentId)
        : this(studentId, string.Empty, 1, 0) { }

    public SolicitationCreateDto(int studentId,
                                 string description,
                                 int statusId,
                                 int requestTypeId)
    {   
        StudentId = studentId;
        Description = description;
        StatusId = statusId;
        RequestTypeId = requestTypeId;
    }
}

namespace StudentCenterWeb.DTOs;

public record SolicitationCreateDto(string StudentId,
                                    string Description,
                                    int RequestTypeId);

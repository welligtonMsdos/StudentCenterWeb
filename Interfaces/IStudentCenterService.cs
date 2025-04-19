using StudentCenterWeb.DTOs;

namespace StudentCenterWeb.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
    Task<StudentCenterBaseDto> GetByIdStudentCenterBase(int id);
    Task<ICollection<SolicitationDto>> GetByStudentId(string studentId);
    Task<ICollection<SolicitationDto>> GetSolicitationsByStatusAndStudentId(int statusId, string studentId);
    Task<ICollection<RequestTypeDto>> GetAllRequestType();
    Task<ResponseDto> SaveSolicitation(SolicitationCreateDto solicitationCreateDto);
}

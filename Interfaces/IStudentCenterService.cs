using StudentCenterWeb.DTOs;

namespace StudentCenterWeb.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
    Task<StudentCenterBaseDto> GetByIdStudentCenterBase(int id);
    Task<ICollection<SolicitationDto>> GetByStudentId(int studentId);
    Task<ICollection<SolicitationDto>> GetSolicitationsByStatusAndStudentId(int statusId, int studentId);
    Task<ICollection<RequestTypeDto>> GetAllRequestType();
}

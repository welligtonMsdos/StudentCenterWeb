using StudentCenterWeb.DTOs;

namespace StudentCenterWeb.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
    Task<StudentCenterBaseDto> GetByIdStudentCenterBase(int id);
}

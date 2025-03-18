using StudentCenterWeb.DTOs;

namespace StudentCenterWeb.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
}

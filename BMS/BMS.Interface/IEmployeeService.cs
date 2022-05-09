using BMS.Entity.Dto;
using BMS.Entity.IBase;
using BMS.Entity.Models;

namespace BMS.Interface
{
    public interface IEmployeeService : IGenericService<Employee, EmployeeDto>
    {
        IResponse<LoginResponseDto> Login(LoginDto login);
        IResponse<EmployeeDto> AddEmployee(EmployeeDto model);
        IResponse<bool> ChangePassword(ChangePasswordDto model);
    }
}
using BMS.Entity.Base;
using BMS.Entity.Dto;
using BMS.Entity.IBase;
using BMS.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public AccountController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Login")]
        public IResponse<LoginResponseDto> Login(LoginDto login)
        {
            try
            {
                return _employeeService.Login(login);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<LoginResponseDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        [HttpPost("ChangePassword")]
        public IResponse<bool> ChangePassword(ChangePasswordDto passwordInfo)
        {
            try
            {
                return _employeeService.ChangePassword(passwordInfo);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<bool>($"Bir hata meydana geldi : {ex.Message}");
            }
        }
    }
}

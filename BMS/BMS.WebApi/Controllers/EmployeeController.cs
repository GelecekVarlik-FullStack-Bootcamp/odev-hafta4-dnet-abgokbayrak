using BMS.Entity.Base;
using BMS.Entity.Dto;
using BMS.Entity.IBase;
using BMS.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService service) 
        {
            _employeeService = service;
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin, Yönetici")]
        public IResponse<EmployeeDto> Add(EmployeeDto model)
        {
            try
            {
                return _employeeService.AddEmployee(model);
            }
            catch(Exception ex)
            {
                return new ErrorResponse<EmployeeDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }
    }
}
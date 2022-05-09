using BMS.Bll.Helper;
using BMS.Bll.Mapper;
using BMS.Dal.Abstract;
using BMS.Entity.Dto;
using BMS.Entity.Exceptions;
using BMS.Entity.IBase;
using BMS.Entity.Models;
using BMS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using static BMS.Bll.Helper.ExceptionHandler;

namespace BMS.Bll
{
    public class EmployeeManager : GenericManager<Employee, EmployeeDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenService _tokenService;
        private readonly Mailing _mailing;
        public EmployeeManager(IServiceProvider service) : base(service)
        {
            _employeeRepository = service.GetService<IEmployeeRepository>();
            _tokenService = service.GetService<ITokenService>();
            _mailing = service.GetService<Mailing>();
        }

        #region Methods 
        public IResponse<EmployeeDto> AddEmployee(EmployeeDto model)
        {
            return Subscribe(() => addEmployeeCallback(model), string.Empty);
        }

        public IResponse<bool> ChangePassword(ChangePasswordDto model)
        {
            return Subscribe(() => changePasswordCallback(model), string.Empty);
        }

        public IResponse<LoginResponseDto> Login(LoginDto login)
        {
            return Subscribe(() => loginCallback(login), "Sisteme başarı ile giriş yapılmıştır.");
        }

        #endregion

        #region Callbacks

        private EmployeeDto addEmployeeCallback(EmployeeDto model)
        {
            var plainPassword = RandomGen.GenerateStr();
            var cipherPassword = MD5.Encrypt(plainPassword);
            model.Password = cipherPassword;
            var response = base.Add(model, true);

            if (response.StatusCode == StatusCodes.Status200OK)
                _mailing.Send(response.Data.Email, $"İş yönetim sistemi hesabınız oluşturulmuştur. Mail adresiniz ve '{plainPassword}' şifresini kullanarak giriş yapabilirsiniz.");

            return response.Data;
        }

        private bool changePasswordCallback(ChangePasswordDto model)
        {
            var employee = _employeeRepository.GetEmployeeByEmail(model.Email);

            if (employee == null)
                throw new NotFound404Exception("Böyle bir personel bulunamadı.");

            if (MD5.Encrypt(model.CurrentPassword) != employee.Password)
                throw new BadRequest400Exception("Geçerli şifrenizi yanlış girdiniz.");

            employee.Password = MD5.Encrypt(model.NewPassword);
            var result = base.Update(ObjectMapper.Mapper.Map<EmployeeDto>(employee), true);
            if (result.StatusCode == StatusCodes.Status200OK)
                return true;
            else
                throw new Exception(result.Message);
        }
       
        private LoginResponseDto loginCallback(LoginDto login)
        {
            var employee = _employeeRepository.Login(login.Email, MD5.Encrypt(login.Password));

            if (employee != null)
            {
                var loginUser = ObjectMapper.Mapper.Map<LoginUserDto>(employee);

                var token = _tokenService.CreateAccessToken(loginUser);

                var employeeInfo = new LoginResponseDto
                {
                    AccessToken = token,
                    LoginUser = loginUser
                };
                return employeeInfo;
            }
            else
                throw new NotAcceptable406Exception("Kullanıcı adı ya da şifreniz yanlış.");
            
        }
    }
        #endregion

}
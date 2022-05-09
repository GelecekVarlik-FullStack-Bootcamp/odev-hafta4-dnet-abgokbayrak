using BMS.Entity.Base;
using BMS.Entity.Dto;
using BMS.Entity.IBase;
using BMS.Entity.Models;
using BMS.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IGenericService<RequestMessage, RequestMessageDto> _requestMessageService;
        
        private readonly int _userId;
        private readonly int _departmentId;
        private readonly int _authorityId;
        public RequestController(IHttpContextAccessor contextAccessor, 
                                 IRequestService requestService,
                                 IGenericService<RequestMessage, RequestMessageDto> requestMessageService)
        {
            _requestService = requestService;
            _requestMessageService = requestMessageService;
            _userId = int.Parse(contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _departmentId = int.Parse(contextAccessor.HttpContext.User.FindFirst("DepartmentId").Value);
            _authorityId = int.Parse(contextAccessor.HttpContext.User.FindFirst("AuthorityId").Value);
        }


        [HttpPost("Add")]
        public IResponse<RequestDto> Add(RequestDto model)
        {
            try
            {
                return _requestService.AddNewRequest(_userId, model);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<RequestDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        [HttpGet("GetRequestsByDepartment")]
        public IResponse<List<RequestForListDto>> GetRequestsByDepartment()
        {
            try
            {
                return _requestService.GetRequestsByDepartment(_departmentId);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<List<RequestForListDto>>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        [HttpGet("AssignRequestToEmployee")]
        public IResponse<bool> AssignRequestToEmployee(int requestId)
        {
            try
            {
                return _requestService.AssignRequestToEmployee(requestId, _userId);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<bool>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        [HttpGet("GetRequestDetail")]
        public IResponse<RequestDetailDto> GetRequestDetail(int requestId)
        {
            try
            {
                return _requestService.GetRequestDetail(requestId);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<RequestDetailDto>($"Bir hata meydana geldi : {ex.Message}");
            }
        }

        [HttpPost("SendMessage")]
        public IResponse<bool> SendMessage(RequestMessageDto message)
        {
            try
            {
                message.SenderId = _userId;
                var result = _requestMessageService.Add(message, true);
                if (result.StatusCode == StatusCodes.Status200OK)
                    return new SuccessResponse<bool>("Mesajınız başarıyla gönderildi.", true);
                else
                    return new ErrorResponse<bool>(result.Message, result.StatusCode, false);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<bool>($"Bir hata meydana geldi : {ex.Message}");
            }
        }
    }
}
using BMS.Entity.Base;
using BMS.Entity.IBase;
using BMS.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BMS.WebApi.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiBaseController<TService, T, TDto> : ControllerBase where TService : IGenericService<T, TDto>
                                                                       where T : EntityBase
                                                                       where TDto : DtoBase
    {
        private readonly TService _service;

        public ApiBaseController(TService service)
        {
            _service = service;
        }

        [HttpGet("Find")]
        public IResponse<TDto> Find(int id)
        {
            try
            {
                return _service.Find(id);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi. {ex.Message}");
            }
        }


        [HttpGet("GetAll")]
        public IResponse<List<TDto>> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new ErrorResponse<List<TDto>>($"Bir hata meydana geldi. {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public IResponse<TDto> Add(TDto model)
        {
            try
            {
                return _service.Add(model, true);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi. {ex.Message}");
            }
        }

        [HttpPost("Update")]
        public IResponse<TDto> Update(TDto model)
        {
            try
            {
                return _service.Update(model, true);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<TDto>($"Bir hata meydana geldi. {ex.Message}");
            }
        }

        [HttpPost("Delete")]
        public IResponse<bool> Delete(int id)
        {
            try
            {
                return _service.DeleteById(id, true);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<bool>($"Bir hata meydana geldi. {ex.Message}");
            }
        }

    }
}
using BMS.Bll.Mapper;
using BMS.Dal.Abstract;
using BMS.Entity.Base;
using BMS.Entity.Dto;
using BMS.Entity.Exceptions;
using BMS.Entity.IBase;
using BMS.Entity.Models;
using BMS.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using static BMS.Bll.Helper.ExceptionHandler;

namespace BMS.Bll
{
    public class RequestManager : GenericManager<Request, RequestDto>, IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestManager(IServiceProvider service) : base(service)
        {
            _requestRepository = service.GetService<IRequestRepository>();
        }

        #region Methods

        public IResponse<RequestDto> AddNewRequest(int requesterId, RequestDto model)
        {
            model.RequesterId = requesterId;
            return base.Add(model, true);
        }

        public IResponse<bool> AssignRequestToEmployee(int requestId, int employeeId)
        {
            return Subscribe(() => assignRequestToEmployeeCallback(requestId, employeeId), "Talep başarı ile size atanmıştır.");
        }

        public IResponse<RequestDetailDto> GetRequestDetail(int requestId)
        {
            return Subscribe(() => getRequestDetailCallback(requestId), string.Empty);
        }

        public IResponse<List<RequestForListDto>> GetRequestsByDepartment(int departmentId)
        {
            return Subscribe(() => getRequestsByDepartmentCallback(departmentId), string.Empty);
        }
        #endregion

        #region Callbacks

        private bool assignRequestToEmployeeCallback(int requestId, int employeeId)
        {
            var request = base.Find(requestId);

            if (request.Data == null)
                throw new NotFound404Exception("Böyle bir talep bulunamadı.");

            if (request.Data.EmployeeId != null)
                throw new BadRequest400Exception("Bu talep daha önce başka bir kullanıcıya atandığından size atanamaz.");

            request.Data.EmployeeId = employeeId;

            var response = base.Update(request.Data, true);
            if (response.StatusCode != StatusCodes.Status200OK)
                throw new Exception(response.Message);

            return true;
        }

        private RequestDetailDto getRequestDetailCallback(int requestId)
        {
            var request = _requestRepository.GetRequestWithDetail(requestId);

            if (request == null)
                throw new NotFound404Exception("Böyle bir talep bulunamadı.");

            var requestDto = ObjectMapper.Mapper.Map<RequestDetailDto>(request);
            requestDto.RequestMessages = request.RequestMessages.Select(x => ObjectMapper.Mapper.Map<RequestMessage, RequestMessageForListDto>(x)).ToList();

            return requestDto;
        }

        private List<RequestForListDto> getRequestsByDepartmentCallback(int departmentId)
        {
            var requests = _requestRepository.GetRequestsByDepartment(departmentId);
            var response = requests.Select(x => ObjectMapper.Mapper.Map<RequestForListDto>(x)).ToList();
            return response;
        }

        #endregion

    }
}

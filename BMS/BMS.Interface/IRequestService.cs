using BMS.Entity.Dto;
using BMS.Entity.IBase;
using BMS.Entity.Models;
using System.Collections.Generic;

namespace BMS.Interface
{
    public interface IRequestService : IGenericService<Request, RequestDto>
    {
        IResponse<RequestDto> AddNewRequest(int requesterId, RequestDto model);
        IResponse<List<RequestForListDto>> GetRequestsByDepartment(int departmentId);
        IResponse<bool> AssignRequestToEmployee(int requestId, int employeeId);
        IResponse<RequestDetailDto> GetRequestDetail(int requestId);
    }
}

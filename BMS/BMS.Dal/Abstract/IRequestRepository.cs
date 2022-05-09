using BMS.Entity.Models;
using System.Collections.Generic;

namespace BMS.Dal.Abstract
{
    public interface IRequestRepository 
    {
        List<Request> GetRequestsByDepartment(int departmentId);
        Request GetRequestWithDetail(int requestId);
    }
}

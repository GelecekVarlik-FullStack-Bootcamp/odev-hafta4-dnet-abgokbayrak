using BMS.Dal.Abstract;
using BMS.Dal.Concrete.EntityFramework.Context;
using BMS.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BMS.Dal.Concrete.EntityFramework.Repository
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(BmsContext context) : base(context)
        {
        }

        public List<Request> GetRequestsByDepartment(int departmentId)
        {
            return dbSet.Include(x => x.Priority)
                        .Include(x => x.Requester)
                        .Where(x => x.DepartmentId == departmentId) 
                        .ToList();
        }

        public Request GetRequestWithDetail(int requestId)
        {
            return dbSet.Include(x => x.Priority)
                        .Include(x => x.Department)
                        .Include(x => x.Employee)
                        .Include(x => x.Requester)
                        .Include(x => x.RequestSubject)
                        .Include(x => x.RequestMessages).ThenInclude(x => x.Sender)
                        .Where(x => x.Id == requestId)
                        .SingleOrDefault();
        }


    }
}

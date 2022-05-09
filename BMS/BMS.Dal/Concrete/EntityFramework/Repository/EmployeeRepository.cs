using BMS.Dal.Abstract;
using BMS.Dal.Concrete.EntityFramework.Context;
using BMS.Entity.Models;
using System.Linq;

namespace BMS.Dal.Concrete.EntityFramework.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BmsContext context) : base(context)
        {
        }

        public Employee Login(string email, string password)
        {
            var employee = dbSet.SingleOrDefault(x => x.Email == email && x.Password == password);
            return employee;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            var employee = dbSet.SingleOrDefault(x => x.Email == email);
            return employee;
        }

    }
}

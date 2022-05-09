using BMS.Entity.Models;

namespace BMS.Dal.Abstract
{
    public interface IEmployeeRepository 
    {
        Employee Login(string email, string password);
        Employee GetEmployeeByEmail(string email);
    }

}

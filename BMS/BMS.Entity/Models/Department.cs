using System;
using System.Collections.Generic;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class Department
    {
        public Department()
        {
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Employees = new HashSet<Employee>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}

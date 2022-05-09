using BMS.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class Employee : EntityBase
    {
        public Employee()
        {
            RequestEmployees = new HashSet<Request>();
            RequestMessages = new HashSet<RequestMessage>();
            RequestRequesters = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int AuthorityId { get; set; }

        public virtual Authority Authority { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Request> RequestEmployees { get; set; }
        public virtual ICollection<RequestMessage> RequestMessages { get; set; }
        public virtual ICollection<Request> RequestRequesters { get; set; }
    }
}

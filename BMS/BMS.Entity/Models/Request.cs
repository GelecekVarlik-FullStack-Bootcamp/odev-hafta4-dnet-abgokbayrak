using BMS.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class Request : EntityBase
    {
        public Request()
        {
            RequestMessages = new HashSet<RequestMessage>();
        }

        public int Id { get; set; }
        public string Topic { get; set; }
        public int DepartmentId { get; set; }
        public int? PriorityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Context { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestSubjectId { get; set; }
        public int RequesterId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual DepartmentSubject RequestSubject { get; set; }
        public virtual Employee Requester { get; set; }
        public virtual ICollection<RequestMessage> RequestMessages { get; set; }
    }
}

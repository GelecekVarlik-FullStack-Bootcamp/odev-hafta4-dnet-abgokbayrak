using BMS.Entity.Base;
using System;

namespace BMS.Entity.Dto
{
    public class RequestDto : DtoBase
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public int DepartmentId { get; set; }
        public int? PriorityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Context { get; set; }
        public int? EmployeeId { get; set; }
        public int RequesterId { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestSubjectId { get; set; }

    }

}

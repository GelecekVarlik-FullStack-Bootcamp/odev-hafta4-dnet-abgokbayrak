using System;
using System.Collections.Generic;

namespace BMS.Entity.Dto
{
    public class RequestDetailDto
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string DepartmentName { get; set; }
        public string PriorityName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Context { get; set; }
        public string EmployeeName { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestSubjectName { get; set; }
        public string RequesterName { get; set; }
        public List<RequestMessageForListDto> RequestMessages { get; set; }
    }
}
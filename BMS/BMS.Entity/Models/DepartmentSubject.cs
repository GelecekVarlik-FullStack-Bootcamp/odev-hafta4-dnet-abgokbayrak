using System;
using System.Collections.Generic;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class DepartmentSubject
    {
        public DepartmentSubject()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}

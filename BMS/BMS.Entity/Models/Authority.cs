using System;
using System.Collections.Generic;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class Authority
    {
        public Authority()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

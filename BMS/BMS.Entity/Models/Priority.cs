using System;
using System.Collections.Generic;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class Priority
    {
        public Priority()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}

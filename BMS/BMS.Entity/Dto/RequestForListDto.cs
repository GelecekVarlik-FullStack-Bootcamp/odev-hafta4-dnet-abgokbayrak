using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Entity.Dto
{
    public class RequestForListDto
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string PriorityName { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequesterName { get; set; }
    }
}

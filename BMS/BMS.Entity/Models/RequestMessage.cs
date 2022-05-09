using BMS.Entity.Base;

#nullable disable

namespace BMS.Entity.Models
{
    public partial class RequestMessage : EntityBase
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }

        public virtual Request Request { get; set; }
        public virtual Employee Sender { get; set; }
    }
}

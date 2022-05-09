using BMS.Entity.Base;
using System.Text.Json.Serialization;

namespace BMS.Entity.Dto
{
    public class RequestMessageDto : DtoBase
    {
        public int RequestId { get; set; }
        [JsonIgnore]
        public int SenderId { get; set; }
        public string Message { get; set; }

    }
}

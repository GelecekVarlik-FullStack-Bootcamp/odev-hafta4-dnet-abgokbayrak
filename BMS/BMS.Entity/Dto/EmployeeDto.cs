using BMS.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BMS.Entity.Dto
{
    public class EmployeeDto : DtoBase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int AuthorityId { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace BMS.Entity.Dto
{
    public class LoginDto 
    {
        [Required]
        [MaxLength(40)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

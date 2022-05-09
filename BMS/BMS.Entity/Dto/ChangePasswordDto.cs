using System.ComponentModel.DataAnnotations;

namespace BMS.Entity.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [MaxLength(40)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}

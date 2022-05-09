namespace BMS.Entity.Dto
{
    public class LoginResponseDto
    {
        public LoginUserDto LoginUser { get; set; }
        public object AccessToken { get; set; }
    }

}

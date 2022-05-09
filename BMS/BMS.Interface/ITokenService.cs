using BMS.Entity.Dto;

namespace BMS.Interface
{
    public interface ITokenService
    {
        string CreateAccessToken(LoginUserDto loginUser);
    }
}

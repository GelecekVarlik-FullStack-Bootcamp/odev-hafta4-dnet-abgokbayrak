using BMS.Entity.Dto;
using BMS.Entity.Statics;
using BMS.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BMS.Bll
{
    public class TokenManager : ITokenService
    {
        IConfiguration _configuration;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAccessToken(LoginUserDto loginUser)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, loginUser.Email),
                new Claim("DepartmentId", loginUser.DepartmentId.ToString()),
                new Claim("AuthorityId", loginUser.AuthorityId.ToString()),
                new Claim(ClaimTypes.Role, AuthorityTypes.GetAuthorityTypes().SingleOrDefault(x => x.Id == loginUser.AuthorityId).Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials,
                notBefore: DateTime.Now,
                claims: claimsIdentity.Claims
             );

            var tokenHandler = new { token = new JwtSecurityTokenHandler().WriteToken(token) };

            return tokenHandler.token;
        }
    }
}

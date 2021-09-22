using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using WalletPlus.Api.Models.Users;
using WalletPlus.Api.Services.Helpers.Constants;

namespace WalletPlus.Api.Services.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public string GenerateSecureSecret()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(EnvironmentVariables.JWT_SECRETKEY);

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("IsBlocked", user.IsBlocked.ToString()),
                new Claim("IsActive", user.IsActive.ToString()),
            });
            var signingCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = EnvironmentVariables.JWT_ISSUER,
                Audience = EnvironmentVariables.JWT_SECRETKEY,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = signingCredentials,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

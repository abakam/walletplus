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

namespace WalletPlus.Api.Services.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        public string Secret { get; }
        public string Issuer { get; }
        public string Audience { get; }
        public TokenHelper(IConfiguration configuration)
        {
            Secret = configuration["JWT:Secret"];
            Issuer = configuration["JWT:Issuer"];
            Audience = configuration["JWT:Audience"];
        }
        public string GenerateSecureSecret()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(Secret);

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
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = signingCredentials,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

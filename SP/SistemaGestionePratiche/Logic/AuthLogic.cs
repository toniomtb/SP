using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SP.Logic.Interfaces;

namespace SP.Logic
{
    public class AuthLogic : IAuthLogic
    { 
        private readonly ILogger _logger;
        public AuthLogic(ILogger logger)
        {
            _logger = logger;
        }

        public string Authenticate(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                _logger.LogInformation("successful login for admin account");
                return GenerateToken();
            }

            _logger.LogWarning("unsuccessful login");
            return null;
        }

        private string GenerateToken()
        {
            var key = Encoding.ASCII.GetBytes("randomkeytobekeptsecret123456789123456789testprova12345678910"); //--> keep it secret
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(60);
            var securityToken = new JwtSecurityToken
            (
                issuer : "Issuer",
                audience : "Audience",
                claims : new List<Claim>(new[] { new Claim("id", "1") }),
                expires : accessTokenExpiration,
                notBefore : DateTime.UtcNow,
                signingCredentials : new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return accessToken;
        }
    }
}

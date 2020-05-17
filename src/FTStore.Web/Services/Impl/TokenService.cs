using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using FTStore.Auth.App.Models;

namespace FTStore.Web.Services.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AuthenticatedUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetTokenDescriptor(user);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(AuthenticatedUser user)
        {
            var secret = _configuration["JWT:Secret"];
            Console.WriteLine(secret);
            var key = Encoding.ASCII.GetBytes(secret);
            IList<Claim> claims = GetClaims(user);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        private IList<Claim> GetClaims(AuthenticatedUser user)
        {
            var userRoles = String.Join(",", user.Claims);
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.EMail));
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Name));
            claims.Add(new Claim(ClaimTypes.Role, userRoles));
            return claims;
        }
    }
}


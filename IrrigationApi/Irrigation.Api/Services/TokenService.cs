using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Irrigation.Core;
using Irrigation.Core.Contracts;
using Irrigation.Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace Irrigation.Api.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(8),
            Subject = GenerateClaims(user)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();
        
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role.Name));

        return ci;
    }
}
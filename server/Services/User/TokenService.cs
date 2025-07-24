using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Verbum.API.DTOs.User;

namespace Verbum.API.Services;

public class TokenService {

    public string GenerateToken(UserDto user) {
        var handler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(Configuration.JWT_SECRET);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = GenerateClaimsIdentity(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaimsIdentity(UserDto user) {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
        return ci;
    }
}
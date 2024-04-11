using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eros.Auth.Models;
using Eros.Domain.Aggregates.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eros.Auth.Services;

public class JwtService(IConfiguration configuration)
{
    private const int ExpirationMinutes = 60 * 24 * 7; // 7 days

    public JwtToken CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new JwtToken()
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresAt = expiration,
            CreatedAt = DateTime.UtcNow
        };
    }

    private JwtSecurityToken CreateJwtToken(
        IEnumerable<Claim> claims,
        SigningCredentials credentials,
        DateTime expiration
    ) =>
        new(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private static Claim[] CreateClaims(User user) =>
        new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("IsAdmin", user.IsAdmin ? "True" : "False")
        };

    private SigningCredentials CreateSigningCredentials()
    {
        var secretKey = configuration["Jwt:SecretKey"] ??
                        throw new Exception("Secret key is missing in configuration.");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        return signingCredentials;
    }
}
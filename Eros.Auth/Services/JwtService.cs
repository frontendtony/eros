using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Eros.Auth.Models;
using Eros.Domain.Aggregates.Estates;
using Eros.Domain.Aggregates.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eros.Auth.Services;

public class JwtService(
  IConfiguration configuration,
  IEstateUserReadRepository estateUserReadRepository
)
{
  private const int ExpirationMinutes = 60 * 24; // 1 days
  private const int RefreshExpirationMinutes = 60 * 24 * 7; // 7 days

  public async Task<JwtToken> CreateToken(User user)
  {
    var accessToken = await CreateAccessToken(user);
    var refreshToken = CreateRefreshToken(user);

    var tokenHandler = new JwtSecurityTokenHandler();

    return new JwtToken
    {
      Token = tokenHandler.WriteToken(accessToken),
      ExpiresAt = accessToken.ValidTo,
      CreatedAt = DateTime.UtcNow,
      RefreshToken = tokenHandler.WriteToken(refreshToken)
    };
  }

  private async Task<JwtSecurityToken> CreateAccessToken(User user)
  {
    var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

    var claims = user.IsAdmin ? CreateAdminClaims(user) : await CreateEstateUserClaims(user);

    return CreateJwtToken(
      claims,
      CreateSigningCredentials(),
      expiration
    );
  }

  private JwtSecurityToken CreateRefreshToken(User user)
  {
    var expiration = DateTime.UtcNow.AddMinutes(RefreshExpirationMinutes);

    var claims = CreateRefreshTokenClaims(user);

    return CreateJwtToken(
      claims,
      CreateSigningCredentials(),
      expiration
    );
  }

  private JwtSecurityToken CreateJwtToken(
    IEnumerable<Claim> claims,
    SigningCredentials credentials,
    DateTime expiration
  )
  {
    return new JwtSecurityToken(
      configuration["Jwt:Issuer"],
      configuration["Jwt:Audience"],
      claims,
      expires: expiration,
      signingCredentials: credentials
    );
  }

  private static Claim[] CreateAdminClaims(User user)
  {
    return new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
      new Claim(ClaimTypes.Email, user.Email!),
      new Claim("IsAdmin", "True")
    };
  }

  private async Task<Claim[]> CreateEstateUserClaims(User user)
  {
    var estateUsers = await estateUserReadRepository.GetEstateUsersByUserIdAsync(user.Id, CancellationToken.None);
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
      new Claim(ClaimTypes.Email, user.Email!),
      new Claim("IsAdmin", "False")
    };

    if (estateUsers.Length == 0) return claims;

    // add a claim for each estate the user is a member of with the permissions they have for each estate
    var estateRoles = new Dictionary<string, Dictionary<string, string[]>>();

    foreach (var membership in estateUsers)
    {
      var estateId = membership.EstateId.ToString();
      var roleId = membership.Role.Id.ToString();
      var permissions = membership.Role.Permissions.Select(p => p.Name).ToArray();

      estateRoles[estateId] = new Dictionary<string, string[]>
      {
        { "RoleId", new[] { roleId } },
        { "Permissions", permissions }
      };
    }

    var estateRolesClaim = new Claim("EstateRoles", JsonSerializer.Serialize(estateRoles));
    claims = claims.Append(estateRolesClaim).ToArray();

    return claims;
  }


  private static Claim[] CreateRefreshTokenClaims(User user)
  {
    return
    [
      new Claim("sub", user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Exp,
        DateTime.UtcNow.AddMinutes(RefreshExpirationMinutes).ToString(CultureInfo.InvariantCulture))
    ];
  }

  private SigningCredentials CreateSigningCredentials()
  {
    var secretKey = configuration["Jwt:SecretKey"] ??
                    throw new Exception("Secret key is missing in configuration.");

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    return signingCredentials;
  }

  public JwtSecurityToken? ValidateRefreshToken(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]!);
    var validationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateIssuer = true,
      ValidIssuer = configuration["Jwt:Issuer"],
      ValidateAudience = true,
      ValidAudience = configuration["Jwt:Audience"],
      ValidateLifetime = true,
      ClockSkew = TimeSpan.Zero
    };

    try
    {
      tokenHandler.ValidateToken(token, validationParameters, out _);

      return tokenHandler.ReadToken(token) as JwtSecurityToken;
    }
    catch
    {
      return null;
    }
  }
}

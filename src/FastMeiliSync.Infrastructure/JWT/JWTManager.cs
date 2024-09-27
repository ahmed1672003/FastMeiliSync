using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastMeiliSync.Shared.Settings;
using Microsoft.IdentityModel.Tokens;

namespace FastMeiliSync.Infrastructure.JWT;

public sealed class JWTManager : IJWTManager
{
    public Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var claims = GetClaims(user);
        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(JwtSettings.Secret)
        );
        var jwtToken = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(JwtSettings.AccessTokenExpireDate),
            signingCredentials: new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return Task.FromResult(accessToken);
    }

    private IReadOnlyCollection<Claim> GetClaims(User user)
    {
        List<Claim> claims = new();

        claims.AddRange(
            user.UserRoles.Select(x => new Claim(nameof(CustomClaimTypes.Roles), x.Role.Name))
        );
        claims.Add(new Claim(nameof(CustomClaimTypes.UserId), user.Id.ToString()));

        return claims;
    }
}

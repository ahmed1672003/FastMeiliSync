﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastMeiliSync.Shared.Settings;
using Microsoft.IdentityModel.Tokens;

namespace FastMeiliSync.Infrastructure.JWT;

public sealed class JWTManager : IJWTManager
{
    public Task<string> GenerateTokenAsync(
        User user,
        Func<User, List<Claim>> getClaims,
        CancellationToken cancellationToken = default
    )
    {
        var claims = getClaims(user);
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
}

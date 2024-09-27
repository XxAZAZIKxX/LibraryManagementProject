using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryManagement.Api.Core;
using LibraryManagement.Api.Core.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagement.Api.Config;

public class JwtConfig : IConfig
{
    public string Issuer { get; }
    public string Audience { get; }
    public TimeSpan TokenLifetime { get; }

    private readonly byte[] _secret;
    private SymmetricSecurityKey GetSymmetricSecurityKey() => new(_secret);

    public JwtConfig(IConfiguration configuration)
    {
        Issuer = configuration.GetValueOrThrow("Jwt:Issuer");
        Audience = configuration.GetValueOrThrow("Jwt:Audience");
        var tokenLifetimeInMinutes = uint.Parse(configuration.GetValueOrThrow("Jwt:TokenLifetime"));
        TokenLifetime = TimeSpan.FromMinutes(tokenLifetimeInMinutes);
        var secretKey = configuration.GetValueOrThrow("Jwt:Secret");
        _secret = CryptographyTools.GetBytes(Encoding.UTF8.GetBytes(secretKey), size:64);
    }

    public TokenValidationParameters TokenValidationParameters =>
        new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            LifetimeValidator = (before, expires, token, parameters) =>
            {
                if (expires is null) return false;
                return DateTime.UtcNow.AddMinutes(1).CompareTo(expires) <= 0;
            }
        };

    public string GenerateJwtToken(Guid userId)
    {
        var jwt = new JwtSecurityToken(issuer: Issuer, expires: DateTime.UtcNow.Add(TokenLifetime),
            claims: [new Claim("userId", userId.ToString())], audience: Audience,
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha512));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public bool IsJwtTokenSignatureValid(string jwtToken)
    {
        try
        {
            var parameters = TokenValidationParameters;
            parameters.LifetimeValidator = null;
            parameters.ValidateLifetime = false;
            new JwtSecurityTokenHandler().ValidateToken(jwtToken, parameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
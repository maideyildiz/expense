using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Services;

namespace ExpenseTracker.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings jwtSettings;
    private readonly IDateTimeProvider dateTimeProvider;
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        this.jwtSettings = jwtOptions.Value;
        this.dateTimeProvider = dateTimeProvider;
    }
    public string GenerateToken(Guid id, string name, string surname, string subscriptionName)
    {
        var key = Encoding.ASCII.GetBytes(this.jwtSettings.SecretKey);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, name),
            new Claim(JwtRegisteredClaimNames.FamilyName, surname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, subscriptionName) // Add user roles if necessary
        };

        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: this.jwtSettings.Issuer,
            expires: this.dateTimeProvider.UtcNow.AddMinutes(this.jwtSettings.ExpiryInMinutes),
            claims: claims,
            audience: this.jwtSettings.Audience,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string RevokeToken(string token)
    {
        return this.GenerateToken(Guid.Parse(token), string.Empty, string.Empty, string.Empty);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(this.jwtSettings.SecretKey);

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = this.jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = this.jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return claimsPrincipal;
        }
        catch
        {
            return null;
        }
    }
}
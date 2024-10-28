using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTracker.Application.Common.Interfaces.Authentication;
using ExpenseTracker.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
        _dateTimeProvider = dateTimeProvider;
    }
    public string GenerateToken(Guid id, string name, string surname)
    {
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, name),
            new Claim(JwtRegisteredClaimNames.FamilyName, surname),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(ClaimTypes.Role, user.Role) // Add user roles if necessary
        };

        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            claims: claims,
            audience: _jwtSettings.Audience,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}